using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TianYu.Core.Queue.RabbitMQ
{
    internal class RabbitBase : IQueue
    {
        #region 基础初始化
        private const string RabbitMQServerConfig = "RabbitMQServerConfig";
        private static List<RabbitMqServerItem> rabbitMqServerItems = null;
        private object LockObj = new object();
        protected RabbitMqService RabbitService = null;
        public RabbitBase()
        {
            InitRabbitServerConfig();
            InitConnectionFactory();
        }
        /// <summary>
        /// 初始化配置
        /// </summary>
        protected void InitRabbitServerConfig()
        {
            string rabbitServerConfig = ConfigurationManager.AppSettings.Get(RabbitMQServerConfig);
            if (string.IsNullOrEmpty(rabbitServerConfig))
            {
                throw new ConfigurationErrorsException("请配置消息组件RabbitMQ服务器配置,配置格式：HostName@UserName@Password@VirtualHost;");
            }
            GetServerConfigItems(rabbitServerConfig);
        }
        /// <summary>
        /// 初始化MQ链接创建工厂
        /// </summary>
        protected void InitConnectionFactory()
        {
            if (rabbitMqServerItems == null || rabbitMqServerItems.Count == 0)
            {
                throw new ConfigurationErrorsException("请配置消息组件RabbitMQ服务器配置,配置格式：HostName@UserName@Password@Enabled@Interval@Heart;");
            }
            //暂时只支持一台MQ服务器地址配置
            var item = rabbitMqServerItems[0];
            lock (LockObj)
            {
                RabbitService = RabbitService ?? new RabbitMqService(item);
            }
        }
        /// <summary>
        /// 解析MQ配置字符串
        /// </summary>
        /// <param name="serverConfig"></param>
        protected void GetServerConfigItems(string serverConfig)
        {
            if (rabbitMqServerItems == null)
            {
                rabbitMqServerItems = new List<RabbitMqServerItem>();
            }
            string[] configItems = serverConfig.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var config in configItems)
            {
                if (!string.IsNullOrEmpty(config))
                {
                    string key = config.GetHashCode().ToString();
                    var item = config.Split('@');
                    var cgItem = new RabbitMqServerItem()
                    {
                        HostName = item[0],
                        UserName = item[1],
                        Password = item[2],
                    };

                    //if (item.Length > 3)
                    //{
                    //    cgItem.AutomaticRecoveryEnabled = item[3];
                    //    cgItem.NetworkRecoveryInterval = item[4];
                    //    cgItem.HeartBeat = item[5];
                    //}
                    rabbitMqServerItems.Add(cgItem);
                }
            }
        }
        #endregion
        public void Dispose()
        {
            rabbitMqServerItems = null;
            RabbitService.Dispose();
        }

    }
}
