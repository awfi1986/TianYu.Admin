using TianYu.Core.Common;
using TianYu.Core.Common.BaseViewModel;
using TianYu.Core.Log;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TianYu.Core.MQSubscribeWinService.Code
{
    public class FailMqMessageHandler
    {
        private const int total = 2;
        private const string logSource = "FailMqMessageHandler";
        /// <summary>
        /// 运行一次MQ错误消息处理业务
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            bool isNext = false;
            DataCenterHelper dataCenter = new DataCenterHelper();
            var failMessages = dataCenter.GetFailMqMessageModels(total, FailMqMessageStatus.Wait);
            if (failMessages != null && failMessages.Count > 0)
            {
                foreach (var item in failMessages)
                {
                    UpdateStatus(item.Code, FailMqMessageStatus.Handlering);
                    ThreadPool.QueueUserWorkItem((obj) =>
                    {
                        Handler((FailMqMessageModel)obj);
                    }, item);
                }
                isNext = total == failMessages.Count;

            }
            else
            {
                LogHelper.LogWarn(logSource, "没有待处理状态的FailMqMessage数据");
            }
            return isNext;
        }
        /// <summary>
        /// 重置处理中的状态为待处理
        /// </summary>
        public void ResetStatus()
        {
            DataCenterHelper dataCenter = new DataCenterHelper();
            var paramList = new List<SqlParameter>{
                new SqlParameter(){ParameterName="@status",Value=0,SqlDbType = System.Data.SqlDbType.Int },
                new SqlParameter(){ParameterName="@status1",Value=1,SqlDbType = System.Data.SqlDbType.Int },
            };
            dataCenter.ExecuteNonQuery("UPDATE FailMqMessage SET STATUS=@status,UpdateTime=GetDate() WHERE STATUS=@status1", paramList.ToArray());
        }
        internal void Handler(FailMqMessageModel model)
        {
            int count = 1;
            var flag = false;
            do
            {
                int defaultInterval = 1000;
                TaskBaseResponse result = null;
                try
                {
                    result = HttpHelper.HttpPost<TaskBaseResponse>(model.ApiUrl, model.MessageContext);
                    LogHelper.LogInfo(logSource, string.Format("MqMessageExecute 调用业务API： {0};{1};{2}", model.ApiUrl, model.MessageContext, result.ToJsonString()));
                }
                catch (Exception ex)
                {
                    if (result.IsNull())
                    {
                        result = new TaskBaseResponse() { Status = System.Net.HttpStatusCode.InternalServerError };
                    }
                    defaultInterval += 500;
                    LogHelper.LogError(logSource, string.Format("MqMessageExecute 调用业务异常API： {0};{1};{2}; ex:{3}", model.ApiUrl, model.MessageContext, result.ToJsonString(), ex.ToString()));
                }
                if (result.Status == System.Net.HttpStatusCode.OK)
                {
                    flag = UpdateStatus(model.Code, FailMqMessageStatus.Success);
                    break;
                }
                else
                {
                    count++;
                    Thread.CurrentThread.Join(defaultInterval * count);
                }

            } while (count <= 5);
            if (!flag)
            {
                UpdateStatus(model.Code, FailMqMessageStatus.HandlerFail, count);
            }
        }
        internal bool UpdateStatus(string code, FailMqMessageStatus status, int count = 0)
        {
            DataCenterHelper dataCenter = new DataCenterHelper();
            var paramList = new List<SqlParameter>{
                new SqlParameter(){ParameterName="@status",Value=(int)status,SqlDbType = System.Data.SqlDbType.Int },
                new SqlParameter(){ParameterName="@code",Value=code,SqlDbType = System.Data.SqlDbType.VarChar,Size=36 },
            };
            if (count == 0)
            {
                return dataCenter.ExecuteNonQuery("UPDATE FailMqMessage SET STATUS=@status,UpdateTime=GetDate() WHERE Code = @code", paramList.ToArray());
            }
            else
            {
                paramList.Add(new SqlParameter() { ParameterName = "@RetryNumber", Value = count, SqlDbType = System.Data.SqlDbType.Int });
                return dataCenter.ExecuteNonQuery("UPDATE FailMqMessage SET STATUS=@status,UpdateTime=GetDate(),RetryNumber=RetryNumber+@RetryNumber WHERE Code = @code", paramList.ToArray());

            }
        }
    }
    public enum FailMqMessageStatus
    {
        /// <summary>
        /// 待处理
        /// </summary>
        Wait = 0,
        /// <summary>
        /// 处理中
        /// </summary>
        Handlering = 1,
        /// <summary>
        /// 处理失败
        /// </summary>
        HandlerFail = 2,
        /// <summary>
        /// 处理成功
        /// </summary>
        Success = 3
    }
}
