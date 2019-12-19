
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TianYu.Core.ScheduleTaskWinService.Code
{
    /// <summary>
    /// 运行任务线程关系
    /// </summary>
    internal class TaskThreadSettings
    {
        /// <summary>
        /// 运行线程名
        /// </summary>
        public string ThreadName { get; set; }
        /// <summary>
        /// 运行线程ID
        /// </summary>
        public int ThreadId { get; set; }
        /// <summary>
        /// 运行任务id
        /// </summary>
        public int TaskId { get; set; }
        /// <summary>
        /// 运行线程
        /// </summary>
        public Thread RunTread { get; set; }
        /// <summary>
        /// 任务对象
        /// </summary>
        public ScheduleTaskConfigModel TaskModel { get; set; }
    }
}
