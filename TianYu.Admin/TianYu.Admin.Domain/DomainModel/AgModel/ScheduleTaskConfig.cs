using TianYu.Admin.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace TianYu.Admin.Domain
{
    /// <summary>
    /// 定时任务配置表
    /// </summary>
    public partial class ScheduleTaskConfig: AggregateRoot
    {
        public ScheduleTaskConfig()
        {
            this.Id= 0;
            this.TaskName= string.Empty;
            this.ApiUrl= string.Empty;
            this.Parameters= string.Empty;
            this.Method= string.Empty;
            this.ExecuteType= 0;
            this.ExceptionNumber= 0;
            this.DiffSeconds= 0;
            this.RunStatus= 0;
            this.Status= 0;
            this.CreateTime= DateTime.Now;
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskName { get; set; }
        /// <summary>
        /// 任务接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// HTTP请求谓词
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 执行方式(0=间隔时间循环执行)
        /// </summary>
        public int ExecuteType { get; set; } = 0;
        /// <summary>
        /// 异常重试次数
        /// </summary>
        public int ExceptionNumber { get; set; }
        /// <summary>
        /// 间隔时间（秒）
        /// </summary>
        public int DiffSeconds { get; set; }
        /// <summary>
        /// 运行状态(0=待运行，1=运行中，2=等待运行，3=异常结束，4=任务完成)
        /// </summary>
        public int RunStatus { get; set; }
        /// <summary>
        /// 任务状态（1=启用，0=禁用）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 最后运行时间
        /// </summary>
        public System.DateTime? LastRunTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

    }
}