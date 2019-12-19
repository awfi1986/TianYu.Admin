using System;
using System.IO;
using System.Text;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RabbitMQ.Client.Events;
using TianYu.Core.Common;
using TianYu.Core.Queue;
using TianYu.Core.Queue.RabbitMQ;

namespace TianYu.Core.UnitTestProject1
{
    [TestClass]
    public class UnitTestMq
    {
        string message = "rabbit mq test message 测试消息 {0}";
        string queueName = "TianYu.QueueName1"; 
        bool exit = false;
        bool[] states = new bool[4] { false, false, false, false };
        object lockObj = new object();
        [TestMethod]
        public void TestRabbitMq()
        {
             ///
            ThreadPool.QueueUserWorkItem((state) => { TestProducer(); });

            ThreadPool.QueueUserWorkItem((state) => { TestConsumerPull(); });

            ThreadPool.QueueUserWorkItem((state) => { TestConsumerSubscribe(); });

            ThreadPool.QueueUserWorkItem((state) => { TestConsumerSubscribe2(); });
            int maxCount = 0;
            while (!exit)
            {
                Thread.Sleep(500);
                exit = states[0] ^ states[1] ^ states[2] ^ states[3];
                if (maxCount > 1000)
                {
                    break;
                }
                maxCount++;
            }
        }

        public void TestProducer()
        {
            IProducer producer = QueueFactory<IProducer>.Create();
            int count = 0;
            do
            {
                producer.Publish(new MqMessageModel() { Content = string.Format(message, "MqMessageModel " + count) });

                count++;
                Thread.Sleep(500);

            }
            while (count <= 10);
            states[0] = true;
        }

        public void TestConsumerPull()
        {
            int n = 0;
            do
            {
                IConsumer consumer = QueueFactory<IConsumer>.Create();
                consumer.Pull(new Action<MqMessageModel>(ConsumeEvent));
                Thread.Sleep(500);
            } while (n < 5);
            states[1] = true;
        }

        public void TestConsumerSubscribe()
        {
            int n = 0;
            do
            {
                IConsumer consumer = QueueFactory<IConsumer>.Create();
                consumer.Subscribe(queueName, true, new Action<MqMessageModel>(ConsumeEvent), false);
                Thread.Sleep(500);
            } while (n < 5);
            states[2] = true;
        }


        public void TestConsumerSubscribe2()
        {
            int n = 0;
            do
            {
                IConsumer consumer = QueueFactory<IConsumer>.Create();
                consumer.Subscribe(new Action<MqMessageModel>(ConsumeEvent));
                Thread.Sleep(500);
            } while (n < 5);
            states[3] = true;
        }
        private void ConsumeEvent(MqMessageModel sender)
        {
            string filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "mqlog.txt");
            var body = sender.Content;
            string message = string.Format("{2}，{1}已接收： {0}", body, "内部机器", sender.CreateTime);
            Console.WriteLine(message);

            lock (lockObj)
            {
                using (StreamWriter sw = new StreamWriter(filepath, true, Encoding.UTF8))
                {
                    sw.WriteLine(message);
                }
            }
        }
    }
    [RabbitMqMessage("TianYu.QueueName", ExchangeName = "TianYu.ExchangeName", IsProperties = false)]
    public class MqMessageModel
    {
        public string Content { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
