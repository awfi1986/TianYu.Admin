using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.ScheduleTaskWinService.Code
{
    /// <summary>
    /// 任务运行状态
    /// </summary>
    internal enum TaskRunOptions
    {
        /// <summary>
        /// 待运行
        /// </summary>
        WaitRun = 0,
        /// <summary>
        /// 运行中
        /// </summary>
        Runing = 1,
        /// <summary>
        /// 等待运行
        /// </summary>
        WaitRuning = 2,
        /// <summary>
        /// 异常结束
        /// </summary>
        ExcetionEnd = 3,
        /// <summary>
        /// 任务完成
        /// </summary>
        Complete = 4
    }
}
