using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TianYu.Core.Common;
using System.Data.SqlClient;

namespace TianYu.Core.ScheduleTaskWinService.Code
{
    internal class DataCenterHelper
    {
        private static string _connectionString = string.Empty;
        internal string ConnectionString
        {
            get
            {
                if (_connectionString.IsNullOrEmpty())
                {
                    _connectionString = ConfigHelper.GetAppsettingValue("connectionString");
                }
                return _connectionString;
            }
        }
        /// <summary>
        /// 获取定时任务配置
        /// </summary>
        /// <param name="status">0=禁用，1=启用</param>
        /// <returns></returns>
        internal List<ScheduleTaskConfigModel> GetScheduleTasks(int status = 1)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string commandText = "select id,taskname,Apiurl,parameters,method,executetype,exceptionNumber,diffSeconds,runstatus,status,lastruntime,createtime from ScheduleTaskConfig where status = @status order by createtime desc";
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@status",
                        SqlDbType = SqlDbType.Int,
                        Size = 4,
                        Value = status
                    });
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            List<ScheduleTaskConfigModel> models = new List<ScheduleTaskConfigModel>();
            foreach (DataRow row in dt.Rows)
            {
                models.Add(ToScheduleTaskConfigModel(row));
            }
            return models;
        }
        /// <summary>
        /// 数据行转化为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal ScheduleTaskConfigModel ToScheduleTaskConfigModel(DataRow row)
        {
            if (row.IsNull())
            {
                return null;
            }
            return new ScheduleTaskConfigModel()
            {
                Id = row["id"].ToInt(),
                ApiUrl = row["ApiUrl"].ToString(),
                CreateTime = row["CreateTime"].ToDateTime(),
                DiffSeconds = row["DiffSeconds"].ToInt(),
                ExceptionNumber = row["ExceptionNumber"].ToInt(),
                ExecuteType = row["ExecuteType"].ToInt(),
                LastRunTime = row["LastRunTime"].ToDateTime(),
                Method = row["Method"].ToString(),
                Parameters = row["Parameters"].ToString(),
                RunStatus = row["RunStatus"].ToInt(),
                Status = row["Status"].ToInt(),
                TaskName = row["TaskName"].ToString()
            };
        }
        /// <summary>
        /// 更新运行状态
        /// </summary>
        /// <param name="runStatus"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal bool UpdateRunStatus(TaskRunOptions runStatus, int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string commandText = "update  ScheduleTaskConfig set runStatus=@runStatus {0} where id=@id";
                if (runStatus == TaskRunOptions.Runing)
                {
                    commandText = string.Format(commandText, ",lastruntime = @lastruntime ");
                }
                else
                {
                    commandText = string.Format(commandText, "");
                }
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@runStatus",
                        SqlDbType = SqlDbType.Int,
                        Size = 4,
                        Value = runStatus
                    });
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@lastruntime",
                        SqlDbType = SqlDbType.DateTime,
                        Size = 8,
                        Value = DateTime.Now
                    });
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@id",
                        SqlDbType = SqlDbType.Int,
                        Size = 4,
                        Value = id
                    });
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}
