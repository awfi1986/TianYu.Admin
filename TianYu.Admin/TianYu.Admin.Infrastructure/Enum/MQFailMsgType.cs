using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Admin.Infrastructure.Enum
{
    /// <summary>
    /// MQ失败消息是否存储枚举
    /// </summary>
    public enum MQFailMsgType
    {
        /// <summary>
        /// 0 禁用
        /// </summary>
        [Description("不存储")]
        NoStorage = 0,
        /// <summary>
        /// 启用
        /// </summary>
        [Description("存储")]
        Storage = 1,
    }

    /// <summary>
    /// 消费方式
    /// </summary>
    public enum MQConsumerType
    {
        /// <summary>
        /// 0 pull 拉取
        /// </summary>
        [Description("pull")]
        Pull = 0,
        /// <summary>
        /// 1 Subscribe 订阅
        /// </summary>
        [Description("Subscribe")]
        Subscribe = 1,
    }
}
