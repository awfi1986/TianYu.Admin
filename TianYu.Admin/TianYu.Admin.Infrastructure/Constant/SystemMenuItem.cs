using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Infrastructure.Constant
{
    public class SystemMenuItem
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public System.DateTime UpdateTime { get; set; }
        /// <summary>
        /// 上级Id
        /// </summary>
        public string ParentCode { get; set; }
        /// <summary>
        /// 权限项编码（ 每个层级占3位）
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
        /// 项目模块
        /// </summary>
        public string BussionModule { get; set; }
        /// <summary>
        /// 地址（route/controller/action 或 controller/action ）
        /// </summary>
        public string ActionUrl { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int? SortCode { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 状态(0=禁用，1=启用)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 是否有权限  1 有  0 无
        /// </summary>
        public int IsPower { get; set; }
    }
}
