
using TianYu.Core.Common;
using TianYu.Core.Common.BaseViewModel;
using TianYu.Core.Log;
using TianYu.Core.Queue;
using TianYu.Core.Queue.RabbitMQ;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TianYu.Core.MQSubscribeWinService.Code
{
    public sealed class TaskWorker
    {
        #region 属性
        private const string logSource = "MQSubscribeWinService";
        public static bool IsServiceStop { get; set; } = false;
        private static List<MQBusinessConfigModel> taskList = new List<MQBusinessConfigModel>();
        private DataCenterHelper dataCenter = new DataCenterHelper();
        private static ConcurrentStack<FailMqMessageModel> FailMqMessageModels = new ConcurrentStack<FailMqMessageModel>();
        /// <summary>
        /// MQ消费者对象
        /// </summary>
        private static List<IConsumer> Consumers { get; set; } = new List<IConsumer>();
        /// <summary>
        /// 定时任务配置信息
        /// </summary>
        private List<MQBusinessConfigModel> TaskList
        {
            get { return taskList; }
        }

        private static List<Thread> threadList = new List<Thread>();

        /// <summary>
        /// 定时pull线程配置信息
        /// </summary>
        private List<Thread> ThreadList
        {
            get { return threadList; }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化数据任务
        /// </summary>
        private void Init()
        {
            LogHelper.LogInfo(logSource, "TianYu.Core.MQSubscribeWinService.TaskWorker.Init() start");
            taskList = dataCenter.GetMQBusinessConfigs();
            if (TaskList != null && TaskList.Count > 0)
            {
                var task = from a in taskList
                           group a by new { a.QueueName, a.RoutingKey, a.Exchange } into res
                           select new MqConsumerItemKey() { Exchange = res.Key.Exchange, QueueName = res.Key.QueueName, RoutingKey = res.Key.RoutingKey };
                foreach (var item in task)
                {
                    RunFactory(item);
                }
            }
            LogHelper.LogInfo(logSource, "TianYu.Core.MQSubscribeWinService.TaskWorker.Init() end");
        }
        [Obsolete("暂时不用")]
        private void ReloadInit(IEnumerable<MQBusinessConfigModel> configs)
        {
            LogHelper.LogInfo(logSource, "TianYu.Core.MQSubscribeWinService.TaskWorker.ReloadInit() start");
            if (configs != null && configs.Count() > 0)
            {
                var task = from a in configs
                           where !taskList.Any(x => x.Id == a.Id)
                           group a by new { a.QueueName, a.RoutingKey, a.Exchange } into res
                           select new MqConsumerItemKey() { Exchange = res.Key.Exchange, QueueName = res.Key.QueueName, RoutingKey = res.Key.RoutingKey };
                foreach (var item in task)
                {
                    RunFactory(item);
                }
                taskList.AddRange(from a in configs
                                  where !taskList.Any(x => x.Id == a.Id)
                                  select a);

            }
            LogHelper.LogInfo(logSource, "TianYu.Core.MQSubscribeWinService.TaskWorker.ReloadInit() end");
        }
        /// <summary>
        /// 根据配置创建消费者
        /// </summary>
        /// <param name="itemKey"></param>
        private void RunFactory(MqConsumerItemKey itemKey)
        {
            //pull 方式消费消息
            var configModel = TaskList.First(t => t.Exchange == itemKey.Exchange && t.RoutingKey == itemKey.RoutingKey && t.QueueName == itemKey.QueueName);
            if (configModel.MqMessageType == 0)
            {
                Thread th = new Thread(() => ParameterizedThreadStart(itemKey));
                th.Name = string.Format("{0}-{1}-{2}", itemKey.Exchange, itemKey.RoutingKey, itemKey.QueueName);
                th.IsBackground = true;
                threadList.Add(th);
            }
            //sub 方式消费消息
            else
            {
                var consumer = QueueFactory<IConsumer>.Create();
                consumer.Subscribe(itemKey.Exchange, itemKey.QueueName, itemKey.RoutingKey, configModel.IsProperties, MqMessageExecute, false);
                Consumers.Add(consumer);

            }
        }

        private void MqMessageExecute(string message, MqDirectKey arg)
        {
            //获取消费消息业务处理配置
            var configModels = TaskList.FindAll(t => t.Exchange == arg.ExchangeName && t.RoutingKey == arg.RoutingKey && t.QueueName == arg.QueueName);

            if (configModels.IsNull() || configModels.Count == 0)
            {
                //记录日志
                LogHelper.LogError(logSource, string.Format("MqMessageExecute 消费者处理消息时未找到业务配置： {0};{1};{2};消息体：{3}", arg.ExchangeName, arg.RoutingKey, arg.QueueName, message));
                return;
            }
            Task.Run(() =>
            {
                //并发处理消息业务
                var parallelResut = Parallel.For(0, configModels.Count, (i) =>
                  {
                      var configModel = configModels[i];
                      int count = 1;
                      bool flag = false;
                      do
                      {
                          int defaultInterval = 200;
                          BaseResponse<TaskBaseResponse> result = null;
                          try
                          {
                              result = HttpHelper.HttpPost<BaseResponse<TaskBaseResponse>>(configModel.ApiUrl, message);
                              LogHelper.LogInfo(logSource, string.Format("MqMessageExecute 调用业务API： {0};{1};{2}", configModel.ApiUrl, message, result.ToJsonString()));
                          }
                          catch (Exception ex)
                          {
                              if (result.IsNull())
                              {
                                  result = new BaseResponse<TaskBaseResponse>() { Status = System.Net.HttpStatusCode.OK, Data = new TaskBaseResponse { Status = System.Net.HttpStatusCode.InternalServerError } };
                              }
                              defaultInterval = 500;
                              LogHelper.LogError(logSource, string.Format("MqMessageExecute 调用业务异常API： {0};{1};{2}; ex:{3}", configModel.ApiUrl, message, result.ToJsonString(), ex.ToString()));
                          }
                          if (result.Status == System.Net.HttpStatusCode.OK && result.Data.Status == System.Net.HttpStatusCode.OK)
                          {
                              flag = true;
                              break;
                          }
                          else
                          {
                              var number = (count - 1) * defaultInterval;
                              Thread.CurrentThread.Join(number);
                          }
                          count++;
                      } while (count <= configModel.ExceptionNumber);
                      if (!flag)
                      {
                          if (configModel.IsSaveFailMessage)
                          {
                              SaveExcetpionMessage(message, configModel.ApiUrl, count);
                          }
                          else
                          {
                              LogHelper.LogWarn(logSource, string.Format("MqMessageExecute 业务异常丢弃消息 API： {0};message:{1};flag:{2};IsSaveFailMessage:{3}", configModel.ApiUrl, message, flag, configModel.IsSaveFailMessage));

                          }
                      }

                  });
            });
        }

        private void ParameterizedThreadStart(MqConsumerItemKey mqItemKey)
        {

            var configModel = TaskList.FirstOrDefault(t => t.Exchange == mqItemKey.Exchange && t.RoutingKey == mqItemKey.RoutingKey && t.QueueName == mqItemKey.QueueName);
            //创建一个消费者
            if (configModel.IsNull())
            {
                //记录日志
                LogHelper.LogInfo(logSource, string.Format("ParameterizedThreadStart 配置PULL方式消费者未找到业务配置： {0};{1};{2}", mqItemKey.Exchange, mqItemKey.RoutingKey, mqItemKey.QueueName));
                return;
            }
            var Consumer = QueueFactory<IConsumer>.Create();
            Consumers.Add(Consumer);
            if (configModel.MqMessageType == 0)
            {
                do
                {
                    Consumer.Pull(mqItemKey.Exchange, mqItemKey.QueueName, mqItemKey.RoutingKey, MqMessageExecute);
                    var timeInterval = configModel.TimeInterval == 0 ? 10 : configModel.TimeInterval;
                    Thread.CurrentThread.Join(timeInterval);
                }
                while (true);
            }
        }

        private void SaveExcetpionMessage(string message, string apiUrl, int retryNumber)
        {
            FailMqMessageModels.Push(new FailMqMessageModel()
            {
                ApiUrl = apiUrl,
                Code = (apiUrl + message).ToMd5(),
                CreateTime = DateTime.Now,
                MessageContext = message,
                RetryNumber = retryNumber,
                Status = 0,
                UpdateTime = DateTime.Now
            });
        }


        #endregion

        /// <summary>
        /// 运行任务
        /// </summary>
        public void Run()
        {
            IsServiceStop = false;
            LogHelper.LogInfo(logSource, "TianYu.Core.MQSubscribeWinService.TaskWorker.Run()");
            this.Init();
            InitSaveFailThread();
            InitFailMessageHandle();
            if (ThreadList == null || ThreadList.Count == 0)
            {
                Thread.CurrentThread.Join(60000);
                this.Init();

            }
            else
            {
                foreach (var item in ThreadList)
                {
                    item.Start();
                }
            }

        }
        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            IsServiceStop = true;
            LogHelper.LogInfo(logSource, "TianYu.Core.MQSubscribeWinService.TaskWorker.Stop()");
            if (Consumers.Count > 0)
            {
                foreach (var item in Consumers)
                {
                    try
                    {
                        item.Dispose();
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(logSource, string.Format("TianYu.Core.MQSubscribeWinService.TaskWorker.Stop();Consumers.Dispose ex:{0}", ex.ToString()));
                    }
                }
            }
            try
            {
                SaveFailMqMeessage();
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logSource, string.Format("TianYu.Core.MQSubscribeWinService.TaskWorker.Stop();SaveFailMqMeessage ex:{0}", ex.ToString()));
            }
            if (threadList != null && threadList.Count > 0)
            {
                foreach (var item in threadList)
                {
                    try
                    {
                        if (item.ThreadState != ThreadState.Stopped)
                        {
                            item.Abort();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(logSource, string.Format("TianYu.Core.MQSubscribeWinService.TaskWorker.Stop();Thread Stop ex:{0}", ex.ToString()));
                    }
                }
            }
            threadList = null;
            taskList = null;
        }
        /// <summary>
        /// 初始化数据存储线程
        /// </summary>
        private void InitSaveFailThread()
        {
            Thread th = new Thread(SaveFailMqMeessage);
            th.Name = "SaveFailMqMeessage Thread";
            th.IsBackground = true;
            ThreadList.Add(th);
        }



        private void SaveFailMqMeessage()
        {
            while (true)
            {
                if (IsServiceStop && FailMqMessageModels.Count == 0)
                {
                    break;
                }
                try
                {
                    LogHelper.LogInfo(logSource, "TianYu.Core.MQSubscribeWinService.TaskWorker.SaveFailMqMeessage()");
                    SaveFailMqMeessage(500);
                }
                catch (Exception ex)
                {
                    LogHelper.LogError(logSource, string.Format("TianYu.Core.MQSubscribeWinService.TaskWorker.SaveFailMqMeessage();ex:{0}", ex.ToString()));
                }
                Thread.CurrentThread.Join(ConfigHelper.GetAppsettingValue("DefaultSaveDataThreadSleepTime").ToInt(1000));
            }

        }

        private void SaveFailMqMeessage(int count)
        {
            FailMqMessageModel[] array = new FailMqMessageModel[FailMqMessageModels.Count >= count ? count : FailMqMessageModels.Count];
            if (FailMqMessageModels.Count > 0)
            {
                FailMqMessageModels.TryPopRange(array, 0, array.Length);
            }
            if (array.Length > 0)
            {
                new DataCenterHelper().WriteToList(array);
            }
        }

        /// <summary>
        /// 初始化错误消息处理任务
        /// </summary>
        private void InitFailMessageHandle()
        {
            Thread th = new Thread(FailMessageHandle);
            th.Name = "FailMessageHandle Thread";
            th.IsBackground = true;
            ThreadList.Add(th);
        }
        /// <summary>
        /// 错误消息处理任务
        /// </summary>
        private void FailMessageHandle()
        {
            var handle = new FailMqMessageHandler();
            //每次任务启动时重置处理状态
            handle.ResetStatus();
            while (true)
            {
                try
                {
                    if (!handle.Run())
                    {
                        Thread.CurrentThread.Join(ConfigHelper.GetAppsettingValue("DefaultFailMessageHandlerThreadSleepTime").ToInt(60) * 1000);
                    }
                    else
                    {
                        //每次正常处理一批数据，休眠500毫秒。
                        Thread.CurrentThread.Join(500);
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.LogError(logSource, ex.ToString());
                }
            }
        }
    }
}
