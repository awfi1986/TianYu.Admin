using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using TianYu.Core.Common;
using TianYu.Core.Log;
using ExchangeType = RabbitMQ.Client.ExchangeType;

namespace TianYu.Core.Queue.RabbitMQ
{
    #region RabbitMQ.Client原生封装类
    /// <summary>
    /// RabbitMQ.Client原生封装类
    /// </summary>
    internal class RabbitMqService : IDisposable
    {
        #region 初始化
        //RabbitMQ建议客户端线程之间不要共用Model，至少要保证共用Model的线程发送消息必须是串行的，但是建议尽量共用Connection。
        private static readonly ConcurrentDictionary<string, IModel> ModelDic =
            new ConcurrentDictionary<string, IModel>();

        private static Dictionary<int, MqDirectKey> RabbitMqMessageRoutingkeyValue = new Dictionary<int, MqDirectKey>();

        private const string RabbitMqAttribute = "RabbitMqMessageAttribute";

        private static IConnection _conn;

        private static readonly object LockObj = new object();

        private static void Open(RabbitMqServerItem config)
        {
            if (_conn != null) return;
            lock (LockObj)
            {
                var factory = new ConnectionFactory
                {
                    //设置主机名
                    HostName = config.HostName,

                    //设置心跳时间
                    RequestedHeartbeat = config.HeartBeat,

                    //设置自动重连
                    AutomaticRecoveryEnabled = config.AutomaticRecoveryEnabled,

                    //重连时间
                    NetworkRecoveryInterval = config.NetworkRecoveryInterval,

                    //用户名
                    UserName = config.UserName,

                    //密码
                    Password = config.Password
                };
                factory.AutomaticRecoveryEnabled = true;
                factory.NetworkRecoveryInterval = new TimeSpan(1000);
                _conn = _conn ?? factory.CreateConnection();
            }
        }

        private static RabbitMqMessageAttribute GetRabbitMqAttribute<T>()
        {
            var typeOfT = typeof(T);
            var rabbitMqAttribute = typeOfT.GetAttribute<RabbitMqMessageAttribute>();
            //if (RabbitMqMessagekeyValuePairs == null)
            //{
            //    RabbitMqMessagekeyValuePairs = new Dictionary<int, RabbitMqMessageAttribute>();
            //    var code = (rabbitMqAttribute.ExchangeName + rabbitMqAttribute.IsProperties).GetHashCode();
            //    RabbitMqMessagekeyValuePairs.Add(code,rabbitMqAttribute);
            //}
            //else {
            //    var typeOfT = typeof(T);
            //    _rabbitMqAttributeList.Find(t => t == typeOfT);

            //}
            return rabbitMqAttribute;
        }

        internal RabbitMqService(RabbitMqServerItem config)
        {
            Open(config);
        }
        #endregion

        #region 交换器声明
        /// <summary>
        /// 交换器声明
        /// </summary>
        /// <param name="iModel"></param>
        /// <param name="exchange">交换器</param>
        /// <param name="type">交换器类型：
        /// 1、Direct Exchange – 处理路由键。需要将一个队列绑定到交换机上，要求该消息与一个特定的路由键完全
        /// 匹配。这是一个完整的匹配。如果一个队列绑定到该交换机上要求路由键 “dog”，则只有被标记为“dog”的
        /// 消息才被转发，不会转发dog.puppy，也不会转发dog.guard，只会转发dog
        /// 2、Fanout Exchange – 不处理路由键。你只需要简单的将队列绑定到交换机上。一个发送到交换机的消息都
        /// 会被转发到与该交换机绑定的所有队列上。很像子网广播，每台子网内的主机都获得了一份复制的消息。Fanout
        /// 交换机转发消息是最快的。
        /// 3、Topic Exchange – 将路由键和某模式进行匹配。此时队列需要绑定要一个模式上。符号“#”匹配一个或多
        /// 个词，符号“*”匹配不多不少一个词。因此“audit.#”能够匹配到“audit.irs.corporate”，但是“audit.*”
        /// 只会匹配到“audit.irs”。</param>
        /// <param name="durable">持久化</param>
        /// <param name="autoDelete">自动删除</param>
        /// <param name="arguments">参数</param>
        private static void ExchangeDeclare(IModel iModel, string exchange, string type = ExchangeType.Direct,
            bool durable = true,
            bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            exchange = string.IsNullOrWhiteSpace(exchange) ? "" : exchange.Trim();
            iModel.ExchangeDeclare(exchange, type, durable, autoDelete, arguments);
        }
        #endregion

        #region 队列声明
        /// <summary>
        /// 队列声明
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="queue">队列</param>
        /// <param name="durable">持久化</param>
        /// <param name="exclusive">排他队列，如果一个队列被声明为排他队列，该队列仅对首次声明它的连接可见，
        /// 并在连接断开时自动删除。这里需要注意三点：其一，排他队列是基于连接可见的，同一连接的不同信道是可
        /// 以同时访问同一个连接创建的排他队列的。其二，“首次”，如果一个连接已经声明了一个排他队列，其他连
        /// 接是不允许建立同名的排他队列的，这个与普通队列不同。其三，即使该队列是持久化的，一旦连接关闭或者
        /// 客户端退出，该排他队列都会被自动删除的。这种队列适用于只限于一个客户端发送读取消息的应用场景。</param>
        /// <param name="autoDelete">自动删除</param>
        /// <param name="arguments">参数</param>
        private static void QueueDeclare(IModel channel, string queue, bool durable = true, bool exclusive = false,
            bool autoDelete = false, IDictionary<string, object> arguments = null)
        {
            queue = string.IsNullOrWhiteSpace(queue) ? "UndefinedQueueName" : queue.Trim();
            channel.QueueDeclare(queue, durable, exclusive, autoDelete, arguments);
        }
        #endregion

        #region 获取Model

        /// <summary>
        /// 获取Model
        /// </summary>
        /// <param name="exchange">交换机名称</param>
        /// <param name="queue">队列名称</param>
        /// <param name="routingKey"></param>
        /// <param name="isProperties">是否持久化</param>
        /// <returns></returns>
        private static IModel GetModel(string exchange, string queue, string routingKey, bool isProperties = false)
        {
            return ModelDic.GetOrAdd(queue, key =>
              {
                  var model = _conn.CreateModel();
                  ExchangeDeclare(model, exchange, ExchangeType.Fanout, isProperties);
                  QueueDeclare(model, queue, isProperties);
                  model.QueueBind(queue, exchange, routingKey);
                  ModelDic[queue] = model;
                  return model;
              });
        }

        /// <summary>
        /// 获取Model
        /// </summary>
        /// <param name="queue">队列名称</param>
        /// <param name="isProperties"></param>
        /// <returns></returns>
        private static IModel GetModel(string queue, bool isProperties = false)
        {
            return ModelDic.GetOrAdd(queue, value =>
             {
                 var model = _conn.CreateModel();
                 QueueDeclare(model, queue, isProperties);

                 //每次消费的消息数
                 model.BasicQos(0, 1, false);

                 ModelDic[queue] = model;

                 return model;
             });
        }
        #endregion

        #region 发布消息

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="command">指令</param>
        /// <returns></returns>
        internal void Publish<T>(T command) where T : class
        {
            var queueInfo = GetRabbitMqAttribute<T>();

            if (queueInfo.IsNull())
                throw new ArgumentException(RabbitMqAttribute);

            var body = command.ToJsonString();
            var exchange = queueInfo.ExchangeName;
            var queue = queueInfo.QueueName;
            var routingKey = queueInfo.ExchangeName;
            var isProperties = queueInfo.IsProperties;

            Publish(exchange, queue, routingKey, body, isProperties);
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="routingKey">路由键</param>
        /// <param name="body">队列信息</param>
        /// <param name="exchange">交换机名称</param>
        /// <param name="queue">队列名</param>
        /// <param name="isProperties">是否持久化</param>
        /// <returns></returns>
        private void Publish(string exchange, string queue, string routingKey, string body, bool isProperties = false)
        {
            var channel = GetModel(exchange, queue, routingKey, isProperties);
            LogHelper.LogInfo("RabbitMQService.Publish", "exchange:{0},routingKey:{1},queue:{3},body:{2}", exchange,routingKey, queue, body);
            channel.BasicPublish(exchange, routingKey, null, body.SerializeUtf8());

        }

        /// <summary>
        /// 发布消息到死信队列
        /// </summary>
        /// <param name="body">死信信息</param>
        /// <param name="ex">异常</param>
        /// <param name="queue">死信队列名称</param>
        /// <returns></returns>
        private void PublishToDead<T>(string queue, string body, Exception ex) where T : class
        {
            var queueInfo = typeof(T).GetAttribute<RabbitMqMessageAttribute>();
            if (queueInfo.IsNull())
                throw new ArgumentException(RabbitMqAttribute);

            var deadLetterExchange = queueInfo.ExchangeName;
            string deadLetterQueue = queueInfo.QueueName;
            var deadLetterRoutingKey = deadLetterExchange;
            var deadLetterBody = new DeadLetterQueue
            {
                Body = body,
                CreateDateTime = DateTime.Now,
                ExceptionMsg = ex.Message,
                Queue = queue,
                RoutingKey = deadLetterExchange,
                Exchange = deadLetterRoutingKey
            };

            Publish(deadLetterExchange, deadLetterQueue, deadLetterRoutingKey, deadLetterBody.ToJsonString());
        }
        #endregion

        #region 订阅消息

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">消费处理</param>
        internal void Subscribe<T>(Action<T> handler) where T : class
        {
            var queueInfo = GetRabbitMqAttribute<T>();
            if (queueInfo.IsNull())
                throw new ArgumentException(RabbitMqAttribute);

            var isDeadLetter = typeof(T) == typeof(DeadLetterQueue);

            Subscribe(queueInfo.QueueName, queueInfo.IsProperties, handler, isDeadLetter);
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queue">队列名称</param>
        /// <param name="isProperties"></param>
        /// <param name="handler">消费处理</param>
        /// <param name="isDeadLetter"></param>
        internal void Subscribe<T>(string queue, bool isProperties, Action<T> handler, bool isDeadLetter) where T : class
        {
            //队列声明
            var channel = GetModel(queue, isProperties);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var msgStr = body.DeserializeUtf8();
                var msg = msgStr.ToJsonObject<T>();

                try
                {

                    handler(msg);

                }
                catch (Exception ex)
                {
                    //ex.GetInnestException().WriteToFile("队列接收消息", "RabbitMq");
                    if (!isDeadLetter)
                        PublishToDead<DeadLetterQueue>(queue, msgStr, ex);
                }
                finally
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            channel.BasicConsume(queue, false, consumer);
        }
        /// <summary>
        /// 将MQ消息交换组成key
        /// </summary>
        /// <param name="exchange"></param>
        /// <param name="queueName"></param>
        /// <param name="routingKey"></param>
        /// <returns></returns>
        internal string MergeRoutingInfo(string exchange, string queueName, string routingKey)
        {
            return string.Format("{0}~{1}~{2}", exchange, routingKey, queueName);
        }
        internal MqDirectKey GetDirectKey(string exchange, string queueName, string routingKey)
        {
            int key = MergeRoutingInfo(exchange, queueName, routingKey).GetHashCode();
            MqDirectKey directKey = null;
            if (RabbitMqMessageRoutingkeyValue.ContainsKey(key))
                directKey = RabbitMqMessageRoutingkeyValue[key];
            else
            {
                directKey = new MqDirectKey()
                {
                    ExchangeName = exchange,
                    QueueName = queueName,
                    RoutingKey = routingKey
                };
                RabbitMqMessageRoutingkeyValue[key] = directKey;
            }
            return directKey;
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="exchange">交换机</param>
        /// <param name="queue">队列名称</param>
        /// <param name="routingKey">路由</param>
        /// <param name="isProperties">是否持久化</param>
        /// <param name="handler">消费处理</param>
        /// <param name="isDeadLetter"></param>
        internal void Subscribe(string exchange, string queue, string routingKey, bool isProperties, Action<string, MqDirectKey> handler, bool isDeadLetter)
        {
            //队列声明
            var channel = GetModel(exchange, queue, routingKey, isProperties);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var msgStr = body.DeserializeUtf8();
                MqDirectKey directKey = GetDirectKey(exchange, queue, routingKey);
                try
                {
                    handler(msgStr, directKey);
                }
                catch (Exception ex)
                {
                    //ex.GetInnestException().WriteToFile("队列接收消息", "RabbitMq");
                    if (!isDeadLetter)
                        PublishToDead<DeadLetterQueue>(queue, msgStr, ex);
                }
                finally
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                }
            };
            channel.BasicConsume(queue, false, consumer);
        }

        #endregion

        #region 获取消息

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">消费处理</param>
        internal void Pull<T>(Action<T> handler) where T : class
        {
            var queueInfo = GetRabbitMqAttribute<T>();
            if (queueInfo.IsNull())
                throw new ArgumentException("RabbitMqAttribute");

            Pull(queueInfo.ExchangeName, queueInfo.QueueName, queueInfo.ExchangeName, handler);
        }

        /// <summary>
        /// 获取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="handler">消费处理</param>
        private void Pull<T>(string exchange, string queue, string routingKey, Action<T> handler) where T : class
        {
            var channel = GetModel(exchange, queue, routingKey);

            var result = channel.BasicGet(queue, false);
            if (ObjectExtension.IsNull(result))
                return;

            var msg = result.Body.DeserializeUtf8().ToJsonObject<T>();
            try
            {
                handler(msg);
            }
            finally
            {
                channel.BasicAck(result.DeliveryTag, false);
            }
        }
        /// <summary>
        /// 获取消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="exchange"></param>
        /// <param name="queue"></param>
        /// <param name="routingKey"></param>
        /// <param name="handler">消费处理</param>
        internal void Pull(string exchange, string queue, string routingKey, Action<string, MqDirectKey> handler)
        {
            var channel = GetModel(exchange, queue, routingKey);
            
            MqDirectKey directKey = GetDirectKey(exchange, queue, routingKey); 
            var result = channel.BasicGet(queue, false);
            if (ObjectExtension.IsNull(result))
                return;
            var msg = result.Body.DeserializeUtf8();
            try
            {
                handler(msg, directKey);
            }
            finally
            {
                channel.BasicAck(result.DeliveryTag, false);
            }
        }

        #endregion

        #region 释放资源
        /// <summary>
        /// 执行与释放或重置非托管资源关联的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            foreach (var item in ModelDic)
            {
                item.Value.Dispose();
            }
            _conn.Dispose();
        }
        #endregion
    }
    #endregion
}
