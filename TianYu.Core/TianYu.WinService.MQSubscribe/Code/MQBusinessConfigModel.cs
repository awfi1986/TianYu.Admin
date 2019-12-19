using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.MQSubscribeWinService.Code
{
    /// <summary>
    /// MQ业务处理配置
    /// </summary>
    internal class MQBusinessConfigModel
    {
 
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 业务名称
        /// </summary>
        public string BusinessName { get; set; }
        /// <summary>
        /// 接口地址
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
        /// 异常重试次数（重试间隔计算方式：（执行次数-1）*默认间隔时间）
        /// </summary>
        public int ExceptionNumber { get; set; }
        /// <summary>
        /// 消息获取方式(0=pull,1=Subscribe)
        /// </summary>
        public int MqMessageType { get; set; }
        /// <summary>
        /// 时间间隔(单位毫秒，仅当消息获取方式为pull时有效)
        /// </summary>
        public int TimeInterval { get; set; }
        /// <summary>
        /// 失败消息存储
        /// </summary>
        public bool IsSaveFailMessage { get; set; }
        /// <summary>
        /// MQ 消息是否持久化
        /// </summary>
        public bool IsProperties { get; set; }
        /// <summary>
        /// 任务状态
        /// </summary>
        public int Status { get; set; }
      
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
