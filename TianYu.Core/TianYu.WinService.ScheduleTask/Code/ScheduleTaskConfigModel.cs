using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.ScheduleTaskWinService.Code
{
    /// <summary>
    /// 定时任务配置
    /// </summary>
    internal class ScheduleTaskConfigModel
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 请求谓词
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 执行类型
        /// </summary>
        public int ExecuteType { get; set; }
        /// <summary>
        /// 异常重试次数（重试间隔计算方式：（执行次数-1）*默认间隔时间）
        /// </summary>
        public int ExceptionNumber { get; set; }
        /// <summary>
        /// 间隔时间
        /// </summary>
        public int DiffSeconds { get; set; }
        /// <summary>
        /// 运行状态
        /// </summary>
        public int RunStatus { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 最后运行时间
        /// </summary>
        public DateTime LastRunTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
