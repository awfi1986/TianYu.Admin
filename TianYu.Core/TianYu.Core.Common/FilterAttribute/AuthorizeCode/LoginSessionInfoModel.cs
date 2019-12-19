using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.FilterAttribute.AuthorizeCode
{
    /// <summary>
    /// 登录会话session信息
    /// </summary>
    public class LoginSessionInfoModel
    {
        /// <summary>
        /// SessinId
        /// </summary>
        public string SessinId { get; set; }
        /// <summary>
        /// 设备号
        /// </summary>
        public string DeviceNo { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 最后刷新时间
        /// </summary>
        public DateTime LashRefresh { get; set; }
        /// <summary>
        /// 有效期（分钟）
        /// </summary>
        public int Expired { get; set; }
        /// <summary>
        /// 状态（0=无效，1=有效）
        /// </summary>
        public int Status { get; set; }
    }
}
