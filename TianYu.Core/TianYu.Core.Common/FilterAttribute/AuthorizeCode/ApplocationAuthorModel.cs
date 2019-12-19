using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.FilterAttribute.AuthorizeCode
{
    public class ApplocationAuthorModel
    {
        public ApplocationAuthorModel()
        {
            this.AppId = string.Empty;
            this.AppSecret = string.Empty;
            this.Status = 0;
            this.CreateTime = DateTime.Now;
            this.ClientType = string.Empty;
        }

        /// <summary>
        /// AppId
        /// </summary>
        public string AppId { get; set; }
        /// <summary>
        /// App密钥
        /// </summary>
        public string AppSecret { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 客户端类型（商家ios，商家微信，商家小调程序……）
        /// </summary>
        public string ClientType { get; set; }

    }
}
