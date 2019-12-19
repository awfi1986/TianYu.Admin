using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Cache
{
    /// <summary>
    /// 缓存帮助对象
    /// 缓存键（命名规范：公司名称:项目名称:key；如:TianYu:userinfos:key1）
    /// </summary>
    public static class CacheHelper
    {
        private static ICache cache = null;
        static CacheHelper()
        {
            cache = new RedisCache();
        }

        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public static int TimeOut
        {
            get
            {
               return  cache.TimeOut;
            }
            set
            {
                if(value > 0)
                {
                    cache.TimeOut = value;
                }
            }
        }
        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        public static object Get(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("KEY不能为空！");
            }
            return cache.Get(key);
        }
        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        public static T Get<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("KEY不能为空！");
            }
            return cache.Get<T>(key);
        }
        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        public static bool Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("KEY不能为空！");
            }
             return cache.Remove(key);
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        public static bool Insert(string key, object data)
        {
            if (string.IsNullOrEmpty(key) || data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
           
            return cache.Insert(key, data);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="defaultTimeOut">是否滑动时间</param>
        public static bool Insert(string key, object data,bool defaultTimeOut)
        {
            if (string.IsNullOrEmpty(key) || data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return cache.Insert(key, data, defaultTimeOut);
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        /// <param name="defaultTimeOut">是否滑动时间</param>
        public static bool Insert(string key, object data, int cacheTime, bool defaultTimeOut)
        {
            if (string.IsNullOrEmpty(key) || data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return cache.Insert(key, data, cacheTime, defaultTimeOut);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="trans">事务对象</param>
        public static Task<bool> InsertAsync(string key, object data, ITransaction trans )
        {
            if (string.IsNullOrEmpty(key) || data == null || trans == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return ((ITrans)cache).InsertAsync(key, data, trans);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        public static bool Insert<T>(string key, T data)
        {
            if (string.IsNullOrEmpty(key) && data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
           
            return cache.Insert(key, data);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中,并指定默认滑动时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        public static bool Insert<T>(string key, T data, bool defaultTimeOut)
        {
            if (string.IsNullOrEmpty(key) && data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return cache.Insert(key, data, defaultTimeOut);
        }

        /// <summary>
        /// 将指定键的对象添加到缓存中,并指定默认滑动时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        /// <param name="defaultTimeOut">是否滑动时间</param>
        public static bool Insert<T>(string key, T data, int cacheTime, bool defaultTimeOut)
        {
            if (string.IsNullOrEmpty(key) && data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return cache.Insert(key, data, cacheTime, defaultTimeOut);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="trans">事务对象</param>
        public static Task<bool> InsertAsync<T>(string key, T data, ITransaction trans )
        {
            if (string.IsNullOrEmpty(key) && data == null || trans == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
           
            return ((ITrans)cache).InsertAsync(key, data, trans);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        public static  bool Insert(string key, object data, int cacheTime)
        {
            if (string.IsNullOrEmpty(key) && data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
           
            return cache.Insert(key, data, cacheTime);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        /// <param name="trans">事务对象</param>
        public static Task<bool> InsertAsync(string key, object data, int cacheTime, ITransaction trans)
        {
            if (string.IsNullOrEmpty(key) && data == null || trans == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
            
            return ((ITrans)cache).InsertAsync(key, data, cacheTime, trans);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        public static  bool Insert<T>(string key, T data, int cacheTime)
        {
            if (string.IsNullOrEmpty(key) && data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
          
            return cache.Insert(key, data, cacheTime);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        /// <param name="trans">事务对象</param>
        public static Task<bool> InsertAsync<T>(string key, T data, int cacheTime, ITransaction trans)
        {
            if (string.IsNullOrEmpty(key) && data == null || trans == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return ((ITrans)cache).InsertAsync(key, data, cacheTime, trans);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static  bool Insert(string key, object data, DateTime cacheTime)
        {
            if (string.IsNullOrEmpty(key) && data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return cache.Insert(key, data, cacheTime);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        /// <param name="trans">事务对象</param>
        public static Task<bool> InsertAsync(string key, object data, DateTime cacheTime, ITransaction trans)
        {
            if (string.IsNullOrEmpty(key) && data == null || trans == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return ((ITrans)cache).InsertAsync(key, data, cacheTime, trans);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        public static   bool Insert<T>(string key, T data, DateTime cacheTime)
        {
            if (string.IsNullOrEmpty(key) && data == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
           
            return cache.Insert(key, data, cacheTime);
        }
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键（命名规范：公司名称:项目名称:key）</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        /// <param name="trans">事务对象</param>
        public static Task<bool> InsertAsync<T>(string key, T data, DateTime cacheTime, ITransaction trans)
        {
            if (string.IsNullOrEmpty(key) && data == null || trans == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }

            return ((ITrans)cache).InsertAsync(key, data, cacheTime, trans);
        }
        /// <summary>
        /// 判断key是否存在
        /// </summary>
        /// <param name="key">（命名规范：公司名称:项目名称:key）</param>
        public static  bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("KEY不能为空！");
            }
            return cache.Exists(key);
        }

        #region 事务处理
        /// <summary>
        /// 开启一个事务对象
        /// </summary>
        /// <returns></returns>
        public static ITransaction BeginTrans()
        {
            return cache.BeginTrans();
        }

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="trans"></param>    
        /// <returns></returns>
        public static bool CommitTrans(ITransaction trans)
        {
            if(trans == null)
            {
                throw new ArgumentNullException("KEY不能为空！");
            }
           return  cache.CommitTrans(trans);
        }

        /// <summary>
        /// 事务执行的条件
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static ConditionResult TransCondition(ITransaction trans, Condition condition)
        {
            if (trans == null || condition == null)
            {
                throw new ArgumentNullException("参数不能为空！");
            }
            return cache.TransCondition(trans,condition);
        }
        #endregion
    }
}
