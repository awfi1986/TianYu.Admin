using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TianYu.Core.Common;
using System.Data.SqlClient;

namespace TianYu.Core.MQSubscribeWinService.Code
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
        internal bool ExecuteNonQuery(string commandText, params SqlParameter[] sqlParameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    if (sqlParameters != null && sqlParameters.Count() > 0)
                    {
                        command.Parameters.AddRange(sqlParameters);
                    }
                    connection.Open();
                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="status">0=禁用，1=启用</param>
        /// <returns></returns>
        internal List<MQBusinessConfigModel> GetMQBusinessConfigs(int status = 1)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string commandText = "select [Id],[BusinessName],[ApiUrl],[RoutingKey],[Exchange],[QueueName],[MqMessageType],[TimeInterval],[ExceptionNumber],[IsProperties],[IsSaveFailMessage],[Status],[CreateTime] from MQBusinessConfig where status = @status";
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
            List<MQBusinessConfigModel> models = new List<MQBusinessConfigModel>();
            foreach (DataRow row in dt.Rows)
            {
                models.Add(ToMQBusinessConfigModel(row));
            }
            return models;
        }
        /// <summary>
        /// 数据行转化为实体
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal MQBusinessConfigModel ToMQBusinessConfigModel(DataRow row)
        {
            if (row.IsNull())
            {
                return null;
            }
            return new MQBusinessConfigModel()
            {
                Id = row["id"].ToInt(),
                ApiUrl = row["ApiUrl"].ToString(),
                CreateTime = row["CreateTime"].ToDateTime(),

                BusinessName = row["BusinessName"].ToString(),

                RoutingKey = row["RoutingKey"].ToString(),

                Exchange = row["Exchange"].ToString(),
                QueueName = row["QueueName"].ToString(),

                ExceptionNumber = row["ExceptionNumber"].ToInt(),

                MqMessageType = row["MqMessageType"].ToInt(),

                TimeInterval = row["TimeInterval"].ToInt(),
                IsSaveFailMessage = row["IsSaveFailMessage"].ToInt() == 1,
                IsProperties = row["IsProperties"].ToInt() == 1

            };
        }
        /// <summary>
        /// 将处理失败的数据记录到db
        /// </summary>
        /// <param name="runStatus"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        internal void WriteToList(IList<FailMqMessageModel> failMqMessageModels)
        {
            var rows = from a in failMqMessageModels select a.GetRow();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection))
                {
                    sqlBulkCopy.BulkCopyTimeout = 30;
                    sqlBulkCopy.BatchSize = 100;
                    sqlBulkCopy.DestinationTableName = "FailMqMessage";
                    connection.Open();
                    sqlBulkCopy.WriteToServerAsync(rows.ToArray());
                }
            }
        }
        internal List<FailMqMessageModel> GetFailMqMessageModels(int topTotal, FailMqMessageStatus status)
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                string commandText = "select top(@total) [Code],[MessageContext],[ApiUrl],[RetryNumber],[Status],[CreateTime],[UpdateTime] from FailMqMessage where Status = @status order by Code";
                using (SqlCommand command = new SqlCommand(commandText, connection))
                {
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@status",
                        SqlDbType = SqlDbType.Int,
                        Size = 4,
                        Value = status
                    });
                    command.Parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@total",
                        SqlDbType = SqlDbType.Int,
                        Size = 4,
                        Value = topTotal
                    });
                    using (SqlDataAdapter sda = new SqlDataAdapter(command))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            List<FailMqMessageModel> models = new List<FailMqMessageModel>();
            foreach (DataRow row in dt.Rows)
            {
                models.Add(ToFailMqMessageModel(row));
            }
            return models;
        }

        private FailMqMessageModel ToFailMqMessageModel(DataRow row)
        {
            if (row.IsNull())
            {
                return null;
            }
            return new FailMqMessageModel()
            {
                Code = row["Code"].ToString(),
                ApiUrl = row["ApiUrl"].ToString(),
                CreateTime = row["CreateTime"].ToDateTime(),
                UpdateTime = row["UpdateTime"].ToDateTime(),
                MessageContext = row["MessageContext"].ToString(),
                RetryNumber = row["RetryNumber"].ToInt(),
                Status = row["Status"].ToInt(0)

            };
        }
    }
}
