using StackExchange.Redis;
using System; 
using System.Configuration; 

namespace TianYu.Core.Cache
{
    public class RedisConfig
    {
        private static ConnectionMultiplexer _instance;
        private static readonly object Locker = new object();
        /// <summary>
        /// Redis处理对象
        /// </summary>
        public ConnectionMultiplexer Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Locker)
                    {
                        if (_instance == null)
                        {
                            _instance = GetManager();
                        }
                    }
                }
                return _instance;
            }
        }

        public IDatabase DB
        {
            get
            {
                return Instance.GetDatabase();
            }
        }

        /// <summary>
        ///  主机列表 格式为 IP:PORT
        /// </summary> 
        /// <returns></returns>
        private static ConnectionMultiplexer GetManager()
        {
            var address = ConfigurationManager.AppSettings["RedisServer"];
            var redisPw = ConfigurationManager.AppSettings["RedisPassWord"];
            var arrAddress = address.Split(';');

            if (arrAddress == null || arrAddress.Length <= 0)
            {
                throw new NullReferenceException("Redis连接字符串不能为空！");
            }
            ConfigurationOptions config = new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                //AllowAdmin = true,
                ConnectTimeout = 15000,
                SyncTimeout = 5000,
                ResponseTimeout = 15000
            };

            EndPointCollection endPoints = new EndPointCollection();

            foreach (var item in arrAddress)
            {
                if (!string.IsNullOrEmpty(redisPw))
                {
                    config.Password = redisPw;
                }
                config.EndPoints.Add(item);
            }
            var connect = ConnectionMultiplexer.Connect(config);

            //链接失败事件处理
            connect.ConnectionFailed += MuxerConnectionFailed;
            //链接配置变更
            connect.ConfigurationChanged += MuxerConfigurationChanged;
            //重新连接
            connect.ConnectionRestored += MuxerConnectionRestored;
            //发生内部错误
            connect.InternalError += MuxerInternalError;
            return connect;
        }

        #region  事件处理函数
        private static void MuxerInternalError(object sender, InternalErrorEventArgs e)
        {
            Console.WriteLine("内部错误:{0}", e.Exception);
        }

        private static void MuxerConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            _instance = GetManager();
            Console.WriteLine("重新连接:{0}", e.EndPoint);
        }

        /// <summary>
        /// 配置文件变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Console.WriteLine("配置文件变更:{0}", e.EndPoint);
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void MuxerConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Console.WriteLine("重新连接：Endpoint failed: " + e.EndPoint + ", " + e.FailureType + (e.Exception == null ? "" : (", " + e.Exception.Message)));
        }
        #endregion
    }
}
