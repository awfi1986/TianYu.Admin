using Newtonsoft.Json;
using StackExchange.Redis;
using System; 
using System.Threading.Tasks;

namespace TianYu.Core.Cache
{
    internal class RedisCache : ICache, ITrans
    { 
        private RedisConfig configHelper;
        private IDatabase db;
        
        JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };
        public RedisCache()
        {
            configHelper = new RedisConfig();
            db = configHelper.DB;
        }
        /// <summary>
        /// 连接超时设置
        /// </summary>
        public int TimeOut { get; set; } = 1200;//默认超时时间（单位秒）
         
        public object Get(string key)
        {
            return Get<object>(key);
        }

        public T Get<T>(string key)
        {
            var cacheValue = db.StringGet(key);
            var value = default(T);
            if (!cacheValue.IsNull)
            {
                var cacheObject = JsonConvert.DeserializeObject<CacheObject<T>>(cacheValue, jsonConfig);
                if (cacheObject.ForceOutofDate)
                    db.KeyExpire(key, new TimeSpan(0, 0, cacheObject.ExpireTime));
                value = cacheObject.Value;
            }

            return value;

        }

        public bool Insert(string key, object data)
        {
            var jsonData = GetJsonData(data, TimeOut, false);
            return db.StringSet(key, jsonData);
        }

        public bool Insert(string key, object data,bool defaultTime)
        {
            var jsonData = GetJsonData(data, TimeOut, defaultTime);
            return db.StringSet(key, jsonData);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，指定默认时间滑动
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        /// <param name="defaultTime">是否滑动时间</param>
        /// <returns></returns>
        public bool Insert(string key, object data, int cacheTime, bool defaultTime)
        {
            var jsonData = GetJsonData(data, cacheTime, defaultTime);
            return db.StringSet(key, jsonData);
        }

        public bool Insert(string key, object data, int cacheTime)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData(data, TimeOut, false);
            return db.StringSet(key, jsonData, timeSpan);
        }

        public bool Insert(string key, object data, DateTime cacheTime)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, false);
            return db.StringSet(key, jsonData, timeSpan);
        }

        public bool Insert<T>(string key, T data)
        {
            var currentTime = DateTime.Now;
   //         var timeSpan = currentTime.AddSeconds(TimeOut) - currentTime;
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            return db.StringSet(key, jsonData);
        }
        public bool Insert<T>(string key, T data, bool defaultTime)
        {
            var currentTime = DateTime.Now;
    //        var timeSpan = currentTime.AddSeconds(TimeOut) - currentTime;
            var jsonData = GetJsonData<T>(data, TimeOut, defaultTime);
            return db.StringSet(key, jsonData);
        }

        public bool Insert<T>(string key, T data, int cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            return db.StringSet(key, jsonData, timeSpan);
        }

        public bool Insert<T>(string key, T data, DateTime cacheTime)
        {
            var currentTime = DateTime.Now;
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            return db.StringSet(key, jsonData, timeSpan);

        }

        string GetJsonData(object data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<object>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject);//序列化对象
        }

        string GetJsonData<T>(T data, int cacheTime, bool forceOutOfDate)
        {
            var cacheObject = new CacheObject<T>() { Value = data, ExpireTime = cacheTime, ForceOutofDate = forceOutOfDate };
            return JsonConvert.SerializeObject(cacheObject, jsonConfig);//序列化对象
        }

        public bool Remove(string key)
        {
            return db.KeyDelete(key, CommandFlags.None);
        }

        /// <summary>
        /// 判断key是否存在
        /// </summary>
        public bool Exists(string key)
        {
            return db.KeyExists(key);
        }
        #region 事务处理
        /// <summary>
        /// 开启一个事务对象
        /// </summary>
        /// <returns></returns>
        public ITransaction BeginTrans()
        {
            return db.CreateTransaction();
        }
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="trans"></param>    
        /// <returns></returns>
        public bool CommitTrans(ITransaction trans)
        {
            return trans.Execute();
        }
        /// <summary>
        /// 事务执行的条件
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public ConditionResult TransCondition(ITransaction trans ,Condition condition)
        {
            return trans.AddCondition(condition);
        }

        public  Task<bool> InsertAsync(string key, object data, ITransaction tran)
        {
            var jsonData = GetJsonData(data, TimeOut, false);
            Task<bool> result =  tran.StringSetAsync(key, jsonData);
            return  result;
        }

        public  Task<bool> InsertAsync(string key, object data, int cacheTime, ITransaction tran)
        {
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData(data, TimeOut, false);
            Task<bool> result = tran.StringSetAsync(key, jsonData, timeSpan);
            return  result;
        }

        public  Task<bool> InsertAsync(string key, object data, DateTime cacheTime, ITransaction tran)
        {
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData(data, TimeOut, false);
            Task<bool> result = tran.StringSetAsync(key, jsonData, timeSpan);
            return  result;
        }
        public  Task<bool> InsertAsync<T>(string key, T data, ITransaction tran)
        {
            var currentTime = DateTime.Now;
            var timeSpan = currentTime.AddSeconds(TimeOut) - currentTime;
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            Task<bool>  result =  tran.StringSetAsync(key, jsonData);

            return  result;
        }

        public  Task<bool> InsertAsync<T>(string key, T data, int cacheTime, ITransaction tran)
        {
            var currentTime = DateTime.Now;
            var timeSpan = TimeSpan.FromSeconds(cacheTime);
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            return   tran.StringSetAsync(key, jsonData, timeSpan);

        }

        public  Task<bool> InsertAsync<T>(string key, T data, DateTime cacheTime, ITransaction tran)
        {
            var currentTime = DateTime.Now;
            var timeSpan = cacheTime - DateTime.Now;
            var jsonData = GetJsonData<T>(data, TimeOut, false);
            return  tran.StringSetAsync(key, jsonData, timeSpan);
           

        }

        #endregion

        class CacheObject<T>
        {
            public int ExpireTime { get; set; }
            public bool ForceOutofDate { get; set; }
            public T Value { get; set; }
        }
    }
}
