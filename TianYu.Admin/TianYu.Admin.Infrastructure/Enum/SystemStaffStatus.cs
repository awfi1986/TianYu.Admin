
using System.ComponentModel;

namespace TianYu.Admin.Infrastructure.Enum
{
    /// <summary>
    /// 系统用户状态
    /// </summary>
    public enum SystemStaffStatus
    {
        /// <summary>
        /// 0 禁用
        /// </summary>
        [Description("禁用")]
        Stop = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("启用")]
        Using = 1,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Del = 2,
    }
}
