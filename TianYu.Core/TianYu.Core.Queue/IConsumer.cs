using RabbitMQ.Client.Events;
using TianYu.Core.Queue.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Queue
{
    public interface IConsumer : IQueue
    {
        #region 订阅消息

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">消费处理</param>
        void Subscribe<T>(Action<T> handler) where T : class; 
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue">队列名称</param>
        /// <param name="isProperties"></param>
        /// <param name="handler">消费处理</param>
        /// <param name="isDeadLetter"></param>
        void Subscribe<T>(string queue, bool isProperties, Action<T> handler, bool isDeadLetter) where T : class;
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="queue">队列名称</param>
        /// <param name="routingKey">路由</param>
        /// <param name="isProperties">是否持久化</param>
        /// <param name="handler">消费处理</param>
        /// <param name="isDeadLetter"></param>
        void Subscribe(string exchange, string queue, string routingKey, bool isProperties, Action<string, MqDirectKey> handler, bool isDeadLetter);
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="queue">队列名称</param>
        /// <param name="routingKey">路由</param>
        /// <param name="isProperties">是否持久化</param>
        /// <param name="handler">消费处理</param>
        /// <param name="isDeadLetter"></param>
        //void Subscribe(string exchange, string queue, string routingKey, bool isProperties, Action<string> handler, bool isDeadLetter);

        #endregion

        #region 获取消息
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">消费处理</param>
        void Pull<T>(Action<T> handler) where T : class;
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="handler">消费处理</param>
        //void Pull(string exchange, string queue, string routingKey, Action<string> handler);
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="handler">消费处理</param>
        void Pull(string exchange, string queue, string routingKey, Action<string, MqDirectKey> handler);
        #endregion

    }
}
