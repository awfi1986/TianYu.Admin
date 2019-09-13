using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Infrastructure.Enum
{
    public enum CommonStatusType
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
    }
}
