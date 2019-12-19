using TianYu.Core.Queue.RabbitMQ; 

namespace TianYu.Core.Queue.MqModel
{
    /// <summary>
    /// 商家店铺统计MQ消息
    /// </summary>
    [RabbitMqMessage("TianYu.QueueName.ShopStoreSummary", ExchangeName = "TianYu.ExchangeName.ShopStoreSummary", RoutingKey = "TianYu.RoutingKey.ShopStoreSummary", IsProperties = true)]
    public class ShopStoreSummaryMqModel : IMqModel
    {
        /// <summary>
        /// 商家Id
        /// </summary>
        public string ShopId { get; set; }
        /// <summary>
        /// 店铺Id
        /// </summary>
        public string StoreId { get; set; }
        /// <summary>
        /// 总余额
        /// </summary>
        public decimal TotalMoney { get; set; }
        /// <summary>
        /// 可用余额
        /// </summary>
        public decimal AvailableMoney { get; set; }
        /// <summary>
        /// 锁定金额
        /// </summary>
        public decimal FreezeBalance { get; set; }
        /// <summary>
        /// 提现金额
        /// </summary>
        public decimal WithdrawalMoney { get; set; }
        /// <summary>
        /// 扫码收入
        /// </summary>
        public decimal SweepQrIncome { get; set; }
        /// <summary>
        /// 分润收入
        /// </summary>
        public decimal ShareProfitIncome { get; set; }
        /// <summary>
        /// 中金金额
        /// </summary>
        public decimal AcsBalance { get; set; }

        /// <summary>
        /// 线上商城收入
        /// </summary>
        public decimal OnlineShopIncome { get; set; }
        /// <summary>
        /// 锁粉数量
        /// </summary>
        public int LockFansCount { get; set; }
    }
}
