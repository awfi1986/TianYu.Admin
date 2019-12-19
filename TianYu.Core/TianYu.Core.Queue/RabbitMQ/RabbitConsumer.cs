using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TianYu.Core.Queue.RabbitMQ
{
    internal class RabbitConsumer : RabbitBase, IConsumer
    {
    

        public void Pull<T>(Action<T> handler) where T : class
        {
            RabbitService.Pull(handler);
        }

        public void Pull(string exchange, string queue, string routingKey, Action<string, MqDirectKey> handler)
        {
            RabbitService.Pull(exchange, queue, routingKey, handler);
        }

        public void Subscribe<T>(Action<T> handler) where T : class
        {
            RabbitService.Subscribe(handler);
        }

        public void Subscribe<T>(string queue, bool isProperties, Action<T> handler, bool isDeadLetter) where T : class
        {
            RabbitService.Subscribe(queue,isProperties, handler,isDeadLetter);
        }

        public void Subscribe(string exchange, string queue, string routingKey, bool isProperties, Action<string, MqDirectKey> handler, bool isDeadLetter)
        {
            RabbitService.Subscribe(exchange, queue, routingKey, isProperties, handler, isDeadLetter);
        }
    }
}
