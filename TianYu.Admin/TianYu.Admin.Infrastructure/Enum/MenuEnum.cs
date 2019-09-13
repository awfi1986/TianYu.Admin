using System.ComponentModel; 

namespace TianYu.Admin.Infrastructure.Enum
{ 
    /// <summary>
    /// 状态(0=禁用，1=启用)
    /// </summary>
    public enum MenuStatus
    {
        /// <summary>
        /// 0 禁用
        /// </summary>
        [Description("禁用")]
        Disable = 0,
        /// <summary>
        /// 1 启用
        /// </summary>
        [Description("启用")]
        Enable = 1,
    }
}
