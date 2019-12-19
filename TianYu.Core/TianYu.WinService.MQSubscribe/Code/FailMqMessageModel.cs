using TianYu.Core.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.MQSubscribeWinService.Code
{
    /// <summary>
    /// 失败消息模型
    /// </summary>
    internal class FailMqMessageModel
    {
        private static DataTable FailMqMessageTable = null;
        public FailMqMessageModel()
        {
            if (FailMqMessageTable.IsNull())
            {
                FailMqMessageTable = new DataTable();
                FailMqMessageTable.Columns.Add("Code", typeof(string));
                FailMqMessageTable.Columns.Add("MessageContext", typeof(string));
                FailMqMessageTable.Columns.Add("ApiUrl", typeof(string));
                FailMqMessageTable.Columns.Add("RetryNumber", typeof(int));
                FailMqMessageTable.Columns.Add("Status", typeof(int));
                FailMqMessageTable.Columns.Add("CreateTime", typeof(DateTime));
                FailMqMessageTable.Columns.Add("UpdateTime", typeof(DateTime));
            }
        }
        /// <summary>
        /// 失败消息编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string MessageContext { get; set; }
        /// <summary>
        /// 业务处理地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 重试次数
        /// </summary>
        public int RetryNumber { get; set; }
        /// <summary>
        /// 状态(0=待处理，1=重新处理中，2=重新处理失败，3=重新处理成功)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        internal DataRow GetRow()
        {
            DataRow row = FailMqMessageTable.Clone().NewRow();
            row["Code"] = Code;
            row["MessageContext"] = MessageContext;
            row["ApiUrl"] = ApiUrl;
            row["RetryNumber"] = RetryNumber;
            row["Status"] = Status;
            row["CreateTime"] = CreateTime;
            row["UpdateTime"] = UpdateTime;
            return row;
        }
    }
}
