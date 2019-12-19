using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common
{
    /// <summary>
    /// 收集系统各功能
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property )]
    public class SystemMenuAttribute:Attribute
    {
        /// <summary>
        /// 系统编码
        /// </summary>
        public string SystemCode { get; set; }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string SystemName { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string ActionName { get; set; }
        public SystemMenuAttribute()
        {

        }
        public SystemMenuAttribute(string SystemName)
        {
            this.SystemName = SystemName;
        }
    }
}
