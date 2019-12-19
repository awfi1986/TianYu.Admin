using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Queue.RabbitMQ
{
    /// <summary>
    /// MQ服务器配置
    /// </summary>
    internal class RabbitMqServerItem
    {
        /// <summary>
        /// RabbitMQ服务器名称或IP地址
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// RabbitMQ服务用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// RabbitMQ服务登录密码
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// 心跳时间
        /// </summary>
        public ushort HeartBeat { get; set; } = 60;
        /// <summary>
        /// 自动重连
        /// </summary>
        public bool AutomaticRecoveryEnabled { get; set; } = true;
        /// <summary>
        /// 重连时间
        /// </summary>
        public TimeSpan NetworkRecoveryInterval { get; set; } = new TimeSpan(60);
    }
    /// <summary>
    /// 死信队列实体
    /// </summary>
    [RabbitMqMessage("dead-letter-{Queue}", ExchangeName = "dead-letter-{exchange}")]
    internal class DeadLetterQueue
    {
        public string Body { get; set; }

        public string Exchange { get; set; }

        public string Queue { get; set; }

        public string RoutingKey { get; set; }

        public int RetryCount { get; set; }

        public string ExceptionMsg { get; set; }

        public DateTime CreateDateTime { get; set; }
    }

    /// <summary>
    /// 交换器类型
    /// </summary>
    internal static class ExchangeType
    {
        public const string Direct = "direct";
        public const string Fanout = "fanout";
        public const string Topic = "topic";
    }
}
