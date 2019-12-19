using TianYu.Core.Queue.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Queue
{
    /// <summary>
    /// MQ工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class QueueFactory<T> where T : IQueue
    {
        private static Dictionary<Type, IQueue> Types = new Dictionary<Type, IQueue>();
        private static bool isInit = false;
        /// <summary>
        /// 创建MQ实例
        /// </summary>
        /// <returns></returns>
        public static T Create()
        {
            if (!isInit)
            {
                Init();
            }
            Type type = typeof(T);
            if (!Types.ContainsKey(type))
            {
                throw new DllNotFoundException("指定接口未找到具体的实现");
            }
            return (T)Types[type];
        }
        private static void Init()
        {
            Types[typeof(IProducer)] = new RabbitProducer();
            Types[typeof(IConsumer)] = new RabbitConsumer();
            isInit = true;
        }
    }
}
