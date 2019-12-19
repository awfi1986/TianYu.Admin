using TianYu.Core.Common.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.BaseEF
{
    /// <summary>
    /// 仓储接口，定义公共的泛型GRUD
    /// </summary>
    /// <typeparam name="TEntity">泛型聚合根，因为在DDD里面仓储只能对聚合根做操作</typeparam>
    public interface IEFBaseRepository<TEntity> where TEntity : BaseAggregateRoot
    {
        #region 属性
        /// <summary>
        /// 获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }
        #endregion

        #region 公共方法
        /// <summary>
        /// 根据lamada表达式查询集合
        /// </summary>
        /// <param name="express">lamada表达式</param>
        /// <returns></returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> express);

        /// <summary>
        /// 插入实体记录
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回影响行数</returns>
        int Insert(TEntity entity);

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <returns> 操作影响的行数 </returns>
        int Insert(IEnumerable<TEntity> entities);

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(object id);

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(TEntity entity);

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(IEnumerable<TEntity> entities);

        /// <summary>
        ///     根据lamada表达式删除对象
        /// </summary>
        /// <param name="selector"> lamada表达式 </param>
        /// <returns> 操作影响的行数 </returns>
        int Delete(Expression<Func<TEntity, bool>> express);

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="fields"> 修改字段名集合</param>
        /// <returns> 操作影响的行数 </returns>
        int Update(TEntity entity, params string[] fields);
        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象集合 </param>
        /// <param name="fields"> 修改字段名集合</param>
        /// <returns> 操作影响的行数 </returns>
        int Update(IEnumerable<TEntity> entitys, params string[] fields);

        ///// <summary>
        /////     更新实体记录
        ///// </summary>
        ///// <param name="entity"> 实体对象 </param>
        ///// <returns> 操作影响的行数 </returns>
        //int Update(TEntity entity ,params string[] fields);

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        TEntity GetByKey(object key);

        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// 异步保存修改
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync();

        #region 增强CURD
        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns>IQueryable查询对象</returns>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda);
        /// <summary>
        /// 根据过滤条件，获取单个实体
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns>实体</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda = null);
        /// <summary>
        /// 根据过滤条件，获取单个实体
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns>实体</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda);
        /// <summary>
        /// 根据过滤条件，判断是否存在数据
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns>实体</returns>
        bool Any(Expression<Func<TEntity, bool>> whereLambda = null);
        /// <summary>
        /// 根据过滤条件，获取数量
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns>实体</returns>
        int Count(Expression<Func<TEntity, bool>> whereLambda = null);
        /// <summary>
        /// 批量更新数据，实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="whereLambda">条件表达式：u =>u.Id==1</param>
        /// <param name="updateLambda">更新表达式：u =>new User{Name="ok"}</param>
        /// <returns>是否添加成功：true是，false否</returns>
        bool BatchUpdate(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TEntity>> updateLambda);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns>是否添加成功：true是，false否</returns>
        bool BatchDelete(Expression<Func<TEntity, bool>> whereLambda);
        #endregion

        #region 分页
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda">排序方式：new OrderByBuilder(a => a.UserName[,true])，true=倒序，默认false正序</param>
        /// <returns></returns>
        IQueryable<TEntity> Find(out int totalCount, int pageIndex = 1, int pageSize = 10, Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda);


        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="query">linq表达式</param>
        /// <returns></returns>
        IQueryable<SEntity> Find<SEntity>(out int totalCount, int pageIndex = 1, int pageSize = 10, IQueryable<SEntity> query = null);
        #endregion

        #region 执行sql语句，简单封装
        /// <summary>
        /// 执行SQL返回相应实体类集合数据
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">参数</param>
        /// <returns>实体类集合数据</returns>
        IQueryable<S> SqlQuery<S>(string SqlCommandText, params object[] paramValues);

        /// <summary>
        /// 执行SQL返回相应实体类集合数据
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">参数</param>
        /// <returns>实体类集合数据</returns>
        IQueryable<TEntity> SqlQuery(string SqlCommandText, params object[] paramValues);

        /// <summary>
        /// 执行SQL返回影响行数
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">参数</param>
        /// <returns>实体类集合数据</returns>
        int SqlExecute(string SqlCommandText, params object[] paramValues);
        /// <summary>
        /// 执行SQL返回DataSet
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">SQL参数</param>
        /// <returns></returns>
        DataSet SqlQueryToDataSet(string SqlCommandText, params object[] paramValues);
        #endregion

        /// <summary>
        /// 使用SqlBulkCopy批量拷贝数据（一般使用在大量数据写入，例如：一次写入1000条）
        /// </summary>
        /// <param name="entitys"></param>
        void BulkInsertCopyAll(IEnumerable<TEntity> entitys);

        #endregion
    }
}
