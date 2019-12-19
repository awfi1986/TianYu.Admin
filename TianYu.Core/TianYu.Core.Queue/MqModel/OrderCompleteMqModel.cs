using TianYu.Core.Queue.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Queue.MqModel
{
    /// <summary>
    /// 订单完成成功MQ消息
    /// </summary>
    [RabbitMqMessage("TianYu.QueueName.OrderComplete", ExchangeName = "TianYu.ExchangeName.OrderComplete", RoutingKey = "TianYu.RoutingKey.OrderComplete", IsProperties = true)]
    public class OrderCompleteMqModel:IMqModel
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 第三方单号
        /// </summary>
        public string TranNo { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 商家id
        /// </summary>
        public string ShopId { get; set; }
        /// <summary>
        /// 店铺id
        /// </summary>
        public string StoreId { get; set; }
        /// <summary>
        /// 消费者id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 是否游客
        /// </summary>
        public int IsVisitor { get; set; }

        /// <summary>
        /// MQ消息创建时间
        /// </summary>
        public DateTime MqTime { get; set; } = DateTime.Now;
    }
}
