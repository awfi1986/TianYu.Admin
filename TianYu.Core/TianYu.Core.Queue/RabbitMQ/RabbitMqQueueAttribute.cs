using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TianYu.Core.Common;

namespace TianYu.Core.Queue.RabbitMQ
{
    /// <summary>
    /// 自定义的RabbitMq队列信息实体特性
    /// </summary>
    public class RabbitMqMessageAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueName"></param>
        public RabbitMqMessageAttribute(string queueName)
        {
            QueueName = queueName ?? string.Empty;
        }

        /// <summary>
        /// 交换机名称
        /// </summary>
        public string ExchangeName { get; set; }
        /// <summary>
        /// 队列名称
        /// </summary>
        public string QueueName { get; private set; }

        private string _RoutingKey = string.Empty;
        /// <summary>
        /// 路由key
        /// </summary>
        public string RoutingKey
        {
            get
            {
                if (_RoutingKey.IsNullOrWhiteSpace())
                {
                    return _RoutingKey;
                }
                else { return ExchangeName; }
            }
            set { _RoutingKey = value; }
        }

        /// <summary>
        /// 是否持久化
        /// </summary>
        public bool IsProperties { get; set; } = false;
    }
}
