using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Infrastructure.Enum
{
    /// <summary>
    /// 系统操作按键
    /// </summary>
    public enum SystemActionButton
    {
        /// <summary>
        /// 查询
        /// </summary>
        [Description("查询")]
        Query =1,
        /// <summary>
        /// 添加
        /// </summary>
        [Description("添加")]
        Insert = 2,
        /// <summary>
        /// 更新
        /// </summary>
        [Description("更新")]
        Update = 3,
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Remove = 4,
        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Audit = 5,
        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印")]
        Print = 6,
        /// <summary>
        /// 导出
        /// </summary>
        [Description("导出")]
        Export = 7,
    }
}
