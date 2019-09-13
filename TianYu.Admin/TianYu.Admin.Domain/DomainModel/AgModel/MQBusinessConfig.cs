using TianYu.Admin.Domain.BaseModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;

namespace TianYu.Admin.Domain
{
    /// <summary>
    /// MQ消费业务处理配置
    /// </summary>
    public partial class MQBusinessConfig: AggregateRoot
    {
        public MQBusinessConfig()
        {
            this.Id= 0;
            this.BusinessName= string.Empty;
            this.ApiUrl= string.Empty;
            this.RoutingKey= string.Empty;
            this.Exchange= string.Empty;
            this.QueueName= string.Empty;
            this.IsProperties= 0;
            this.MqMessageType= 0;
            this.TimeInterval= 0;
            this.ExceptionNumber= 0;
            this.IsSaveFailMessage= 0;
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
        public string BusinessName { get; set; }
        /// <summary>
        /// 任务接口地址
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// MQ路由
        /// </summary>
        public string RoutingKey { get; set; }
        /// <summary>
        /// MQ交换机
        /// </summary>
        public string Exchange { get; set; }
        /// <summary>
        /// MQ队列
        /// </summary>
        public string QueueName { get; set; }
        /// <summary>
        /// MQ持久化
        /// </summary>
        public int IsProperties { get; set; }
        /// <summary>
        /// 消息获取方式(0=pull,1=Subscribe)
        /// </summary>
        public int MqMessageType { get; set; }
        /// <summary>
        /// 时间间隔(单位毫秒，仅当消息获取方式为pull时有效)
        /// </summary>
        public int TimeInterval { get; set; }
        /// <summary>
        /// 异常重试次数
        /// </summary>
        public int ExceptionNumber { get; set; }
        /// <summary>
        /// 失败消息存储(0=不存储，1=存储)
        /// </summary>
        public int IsSaveFailMessage { get; set; }
        /// <summary>
        /// 任务状态（1=启用，0=禁用）
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }

    }
}