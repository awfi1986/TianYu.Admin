using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Queue.RabbitMQ
{
    internal class RabbitProducer : RabbitBase, IProducer
    {
        public void Publish<T>(T command) where T : class
        {
            this.RabbitService.Publish(command);
        }
    }
}
