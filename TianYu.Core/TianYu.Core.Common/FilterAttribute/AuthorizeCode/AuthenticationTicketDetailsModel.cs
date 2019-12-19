using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.FilterAttribute.AuthorizeCode
{
    /// <summary>
    /// 接口访问票据表
    /// </summary>
    public class AuthenticationTicketDetailsModel
    {
        public AuthenticationTicketDetailsModel()
        {
            this.TicketId = Guid.NewGuid();
            this.Ticket = string.Empty;
            this.TicketAppID = string.Empty;
            this.TicketSecond = 0;
            this.LastRefreshDate = DateTime.Now;
            this.DeviceNo = string.Empty;
            this.ClientType = string.Empty;
            AppSecret = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 票据编号
        /// </summary>
        public System.Guid TicketId { get; set; }
        /// <summary>
        /// 票据
        /// </summary>
        public string Ticket { get; set; }
        /// <summary>
        /// 授权公玥
        /// </summary>
        public string TicketAppID { get; set; }
        /// <summary>
        /// 票据有效期
        /// </summary>
        public int TicketSecond { get; set; }
        /// <summary>
        /// 票据刷新时间
        /// </summary>
        public System.DateTime LastRefreshDate { get; set; }
        /// <summary>
        /// 访问设备唯一编号
        /// </summary>
        public string DeviceNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 客户端类型（android，ios，wechat，winservice）
        /// </summary>
        public string ClientType { get; set; }
        /// <summary>
        /// App密钥
        /// </summary>
        public string AppSecret { set; get; }



    }
}
