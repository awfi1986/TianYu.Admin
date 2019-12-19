using TianYu.Core.Queue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.MQSubscribeWinService.Code
{
    internal class MqConsumerItemKey
    {
        /// <summary>
        /// MQ路由
        /// </summary>
        internal string RoutingKey { get; set; }
        /// <summary>
        /// MQ交换机
        /// </summary>
        internal string Exchange { get; set; }
        /// <summary>
        /// MQ队列
        /// </summary>
        internal string QueueName { get; set; }

    }
}
