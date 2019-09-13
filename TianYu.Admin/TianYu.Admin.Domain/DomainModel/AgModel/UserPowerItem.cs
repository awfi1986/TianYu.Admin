using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TianYu.Admin.Domain.DomainModel.AgModel
{
    /// <summary>
    /// 用户权限项
    /// </summary>
    public class UserPowerItem
    {
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
        /// <summary>
        /// 子项
        /// </summary>
        public IList<UserPowerItem> ChaildenItems { get; set; }
    }
}