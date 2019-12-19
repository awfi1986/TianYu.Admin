
using TianYu.Core.Common;
using TianYu.Core.Common.BaseViewModel;
using TianYu.Core.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;

namespace TianYu.Core.ScheduleTaskWinService.Code
{
    public sealed class TaskWorker
    {
        #region 属性
        private const string logSource = "ScheduleTaskWinService";
        private static List<ScheduleTaskConfigModel> taskList = new List<ScheduleTaskConfigModel>();
        private DataCenterHelper dataCenter = new DataCenterHelper();
        /// <summary>
        /// 定时任务配置信息
        /// </summary>
        private List<ScheduleTaskConfigModel> TaskList
        {
            get { return taskList; }
        }

        private static List<TaskThreadSettings> threadList = new List<TaskThreadSettings>();

        /// <summary>
        /// 定时任务线程配置信息
        /// </summary>
        private List<TaskThreadSettings> ThreadList
        {
            get { return threadList; }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 初始化任务
        /// </summary>
        private void Init()
        {
            LogHelper.LogInfo(logSource, "TianYu.Core.ScheduleTaskWinService.TaskWorker.Init() start");
            taskList = dataCenter.GetScheduleTasks();
            if (TaskList != null && TaskList.Count > 0)
            {
                foreach (var task in TaskList)
                {
                    RunFactory(task);
                }
            }
            LogHelper.LogInfo(logSource, "TianYu.Core.ScheduleTaskWinService.TaskWorker.Init() end");

        }
        /// <summary>
        /// 任务处理工厂
        /// </summary>
        /// <param name="taskModel"></param>
        private void RunFactory(ScheduleTaskConfigModel taskModel)
        {
            string key = string.Format("TaskWorkerActivatorType_RunFactory_{0}", taskModel.Id);
            try
            {
                var task = ThreadList.FirstOrDefault(t => t.TaskId == taskModel.Id);
                if (!ThreadList.Any(t => t.TaskId == taskModel.Id))
                {
                    InitWorkerThread(taskModel);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logSource, string.Format("初始化运行任务异常:ScheduleTaskConfigModel:{0}", JsonHelper.SerializeObject(taskModel)), ex.ToString());
            }
        }
        /// <summary>
        /// 初始化执行任务线程
        /// </summary>
        /// <param name="task">执行任务接口</param>
        /// <param name="taskModel">任务配置</param>
        private void InitWorkerThread(ScheduleTaskConfigModel taskModel)
        {
            Thread th = new Thread(() =>
            {

                while (true)
                {
                    try
                    {   //更新数据状态
                        UpdateRunStatus(taskModel, TaskRunOptions.Runing);
                        var rm = new TaskBaseResponse();
                        int count = 0;
                        do
                        {
                            int sleepSeconds = (count - 1) * 2000;
                            if (sleepSeconds > 0)
                            {
                                //休眠任务等待下次执行
                                Thread.Sleep(sleepSeconds);
                            }
                            //执行任务
                            try
                            {
                                rm = Execute(taskModel.ApiUrl, taskModel.Parameters, taskModel.Method);
                                if (rm.Status == System.Net.HttpStatusCode.OK)
                                {

                                    //更新数据状态
                                    UpdateRunStatus(taskModel, TaskRunOptions.Complete);
                                    break;
                                }
                                else
                                {
                                    LogHelper.LogError(logSource, string.Format("执行任务{0}异常,id:{1}；任务即将结束；任务执行返回数据：{2}", taskModel.TaskName, taskModel.Id, JsonHelper.SerializeObject(rm)));
                                }
                            }
                            catch (Exception ex)
                            {
                                UpdateRunStatus(taskModel, TaskRunOptions.ExcetionEnd);

                                LogHelper.LogError(logSource, string.Format("执行任务{0}异常,id:{1};API:{2}；ex:{3}", taskModel.TaskName, taskModel.Id,
                                    string.Format("{0},{1},{2}", taskModel.ApiUrl, taskModel.Parameters, taskModel.Method),
                                    ex.ToString()));
                                //休眠 N 秒后重试

                            }
                            count++;
                        } while (count <= taskModel.ExceptionNumber);
                        //休眠任务等待下次执行
                        Thread.Sleep(taskModel.DiffSeconds * 1000);
                    }
                    catch (Exception ex)
                    {
                        UpdateRunStatus(taskModel, TaskRunOptions.ExcetionEnd);
                        LogHelper.LogError(logSource, string.Format("执行任务{0}异常,id:{1}；ex:{2}", taskModel.TaskName, taskModel.Id, ex.ToString()));
                    }
                }
            });
            th.Name = taskModel.TaskName;
            th.IsBackground = true;
            threadList.Add(new TaskThreadSettings()
            {
                RunTread = th,
                TaskId = taskModel.Id,
                ThreadId = th.ManagedThreadId,
                ThreadName = th.Name,
                TaskModel = taskModel
            });

        }

        private void UpdateRunStatus(ScheduleTaskConfigModel taskModel, TaskRunOptions runType)
        {
            try
            {
                DataCenterHelper dataCenter = new DataCenterHelper();
                dataCenter.UpdateRunStatus(runType, taskModel.Id);
            }
            catch (Exception ex)
            {
                LogHelper.LogError(logSource, string.Format("UpdateRunStatus 执行任务{0}异常,id:{1}；ex:{2}", taskModel.TaskName, taskModel.Id, ex.ToString()));
            }
        }
        private TaskBaseResponse Execute(string apiUrl, string parameters, string method)
        {
            bool isPost = true;
            if (method.Equals("get", StringComparison.InvariantCultureIgnoreCase))
            {
                isPost = false;
            }
            LogHelper.LogInfo(logSource, string.Format("Execute 任务{0};{1};{2}", apiUrl, parameters, method));
            if (isPost)
            {
                var result = HttpHelper.HttpPost<TaskBaseResponse>(apiUrl, parameters);
                return result;
            }
            else
            {
                var result = HttpHelper.HttpGet<TaskBaseResponse>(apiUrl, parameters);
                return result;
            }
        }
        #endregion

        /// <summary>
        /// 运行任务
        /// </summary>
        public void Run()
        {
            LogHelper.LogInfo(logSource, "TianYu.Core.ScheduleTaskWinService.TaskWorker.Run()");
            this.Init();

            if (ThreadList == null || ThreadList.Count == 0)
            {
                Thread.CurrentThread.Join(60000);
                this.Init();
            }
            else
            {
                foreach (var item in ThreadList)
                {
                    item.TaskModel.LastRunTime = DateTime.Now;
                    item.RunTread.Start();
                }
            }

        }
        /// <summary>
        /// 停止任务
        /// </summary>
        public void Stop()
        {
            LogHelper.LogInfo(logSource, "TianYu.Core.ScheduleTaskWinService.TaskWorker.Stop()");
            if (threadList != null && threadList.Count > 0)
            {
                foreach (var item in threadList)
                {
                    try
                    {
                        if (item.RunTread.ThreadState != ThreadState.Stopped)
                        {
                            item.RunTread.Abort();
                        }
                    }
                    catch (Exception ex)
                    {
                        LogHelper.LogError(logSource, "TianYu.Core.ScheduleTaskWinService.TaskWorker.Stop() EX:" + ex.ToString());

                    }
                }
            }
            threadList = null;
            taskList = null;
        }
    }
}
