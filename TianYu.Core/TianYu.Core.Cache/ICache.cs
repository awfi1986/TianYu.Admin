using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 缓存过期时间
        /// </summary>
        int TimeOut { set; get; }
        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存值</returns>
        object Get(string key);
        /// <summary>
        /// 获得指定键的缓存值
        /// </summary>
        T Get<T>(string key);
        /// <summary>
        /// 从缓存中移除指定键的缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        bool Remove(string key);

        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        bool Insert(string key, object data);

        /// <summary>
        /// 将指定键的对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        bool Insert<T>(string key, T data);
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        bool Insert(string key, object data, int cacheTime);
        /// <summary>
        /// 将指定键的对象添加到缓存中，指定默认时间滑动
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="defaultTime"></param>
        /// <returns></returns>
        bool Insert(string key, object data, bool defaultTime);
        /// <summary>
        /// 将指定键的对象添加到缓存中，指定默认时间滑动
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        /// <param name="defaultTime">是否滑动时间</param>
        /// <returns></returns>
        bool Insert(string key, object data, int cacheTime, bool defaultTime);

        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间(秒钟)</param>
        bool Insert<T>(string key, T data, int cacheTime);
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        bool Insert(string key, object data, DateTime cacheTime);
        /// <summary>
        /// 将指定键的对象添加到缓存中，并指定过期时间
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存值</param>
        /// <param name="cacheTime">缓存过期时间</param>
        bool Insert<T>(string key, T data, DateTime cacheTime);
        /// <summary>
        /// 将指定键的对象添加到缓存中，指定默认时间滑动
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="defaultTime"></param>
        /// <returns></returns>
        bool Insert<T>(string key, T data, bool defaultTime);
        /// <summary>
        /// 判断key是否存在
        /// </summary>
        bool Exists(string key);

        #region 事务
        /// <summary>
        /// 开启一个事务对象
        /// </summary>
        /// <returns></returns>
        ITransaction BeginTrans();

        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="trans"></param>    
        /// <returns></returns>
        bool CommitTrans(ITransaction trans);

        /// <summary>
        /// 事务执行的条件
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        ConditionResult TransCondition(ITransaction trans, Condition condition);

        #endregion

    }
}
