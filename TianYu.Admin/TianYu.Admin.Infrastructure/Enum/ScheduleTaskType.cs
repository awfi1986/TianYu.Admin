using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Infrastructure.Enum
{
    /// <summary>
    /// 运行状态(0=待运行，1=运行中，2=等待运行，3=异常结束，4=任务完成)
    /// </summary>
    public enum ScheduleTaskStatusType
    {
        /// <summary>
        /// 0 禁用
        /// </summary>
        [Description("待运行")]
        Wait = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("运行中")]
        Running = 1,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("等待运行")]
        WaitRunning = 2,
        /// <summary>
        /// 异常结束
        /// </summary>
        [Description("异常结束")]
        ExceptionEnd = 3,
        /// <summary>
        /// 任务完成
        /// </summary>
        [Description("任务完成")]
        End = 4,
    }

    /// <summary>
    /// 执行方式枚举
    /// </summary>
    public enum TaskExecuteType
    {
        /// <summary>
        /// 0 间隔时间循环执行
        /// </summary>
        [Description("间隔时间循环执行")]
        Periodicity = 0
    }

}
