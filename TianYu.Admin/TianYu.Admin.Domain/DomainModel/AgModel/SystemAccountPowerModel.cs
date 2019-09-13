using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Domain.DomainModel.AgModel
{
    /// <summary>
    /// 用户权限视图
    /// </summary>
    public class SystemAccountPowerModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        
        public string UserName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 掩码
        /// </summary>
        public string MaskCode { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string MobilePhone { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }


        /// <summary>
        /// 权限项id
        /// </summary>
        public int PowerId { get; set; }
        /// <summary>
        /// 上级id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 权限项编码
        /// </summary>
        public string PowerCode { get; set; }

     /// <summary>
     /// 权限项名称
     /// </summary>
        public string PowerName { get; set; }

       /// <summary>
       /// 图标
       /// </summary>
        public string IconCode { get; set; }
        /// <summary>
        /// 业务模块
        /// </summary>
        public string BussionModule { get; set; }
       /// <summary>
       /// 权限地址
       /// </summary>
        public string ActionUrl { get; set; }
        /// <summary>
        /// 排序编码
        /// </summary>
        public int? SortCode { get; set; }

       /// <summary>
       /// 描述
       /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 权限项状态
        /// </summary>
        public int PowerStatus { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public bool IsCheck { get; set; } = false;
    }
}
