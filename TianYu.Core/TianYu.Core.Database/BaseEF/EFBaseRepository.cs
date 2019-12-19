using EntityFramework.Extensions;
using LinqKit;
using TianYu.Core.Common.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions; 
using System.Threading.Tasks;

namespace TianYu.Core.Common.BaseEF
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class EFBaseRepository<TEntity> : IEFBaseRepository<TEntity> where TEntity : BaseAggregateRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public IEFUnitOfWork UnitOfWork { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="unitOfWork"></param>
        public EFBaseRepository(IEFUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        /// <summary>
        /// 
        /// </summary>
        IQueryable<TEntity> IEFBaseRepository<TEntity>.Entities
        {
            get
            {
                return UnitOfWork.context.Set<TEntity>();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual TEntity GetByKey(object key)
        {
            return UnitOfWork.context.Set<TEntity>().Find(key);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> express)
        {
            return Set().AsExpandable().Where(express);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Insert(TEntity entity)
        {
            UnitOfWork.RegisterNew(entity);
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Insert(IEnumerable<TEntity> entities)
        {
            foreach (var obj in entities)
            {
                UnitOfWork.RegisterNew(obj);
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual int Delete(object id)
        {
            var obj = UnitOfWork.context.Set<TEntity>().Find(id);
            if (obj == null)
            {
                return 0;
            }
            UnitOfWork.RegisterDeleted(obj);
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual int Delete(TEntity entity)
        {
            UnitOfWork.RegisterDeleted(entity);
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public virtual int Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                UnitOfWork.RegisterDeleted(entity);
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="express"></param>
        /// <returns></returns>
        public virtual int Delete(Expression<Func<TEntity, bool>> express)
        {
            var lstEntity = Set().AsExpandable().Where(express);
            foreach (var entity in lstEntity)
            {
                UnitOfWork.RegisterDeleted(entity);
            }
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public virtual int Update(TEntity entity, params string[] fields)
        {
            UnitOfWork.RegisterModified(entity, fields);
            return 1;
        }

        /// <summary>
        /// 更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象集合 </param>
        /// <param name="fields"> 修改字段名集合</param>
        /// <returns> 操作影响的行数 </returns>
        public int Update(IEnumerable<TEntity> entitys, params string[] fields)
        {
            foreach (var entity in entitys)
            {
                UnitOfWork.RegisterModified(entity, fields);
            }
            return entitys.Count();
        }
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return UnitOfWork.SaveChanges();
        }
        /// <summary>
        /// 异步保存修改
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return UnitOfWork.SaveChangesAsync();
        }

        #region 基础设置
        /// <summary>
        /// 获取当前DbSet
        /// </summary>
        /// <returns></returns>
        public DbSet<TEntity> Set()
        {
            return UnitOfWork.context.Set<TEntity>();
        }
        #endregion
        #region 增强CURD
        /// <summary>
        /// 根据过滤条件，获取记录
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns>IQueryable查询对象</returns>
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda)
        {
            var query = Set().AsExpandable();
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }

            if (orderLambda != null)
            {
                query = query.OrderBy(orderLambda);
            }
            return query.AsNoTracking().AsQueryable();
        }
        /// <summary>
        /// 根据过滤条件，获取单个实体
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns>IQueryable查询对象</returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda = null)
        {
            var query = Set().AsExpandable();
            return query.AsNoTracking().FirstOrDefault(whereLambda);
        }
        /// <summary>
        /// 根据过滤条件，获取单个实体
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <param name="orderLambda">排序条件</param>
        /// <returns>实体</returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda)
        {
            var query = Set().AsExpandable();
            if (orderLambda != null)
            {
                query = query.OrderBy(orderLambda);
            }
            return query.AsNoTracking().FirstOrDefault(whereLambda);
        }
        /// <summary>
        /// 根据过滤条件，判断是否存在数据
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns>IQueryable查询对象</returns>
        public bool Any(Expression<Func<TEntity, bool>> whereLambda = null)
        {
            var query = Set().AsExpandable();
            return query.Any(whereLambda);
        }
        /// <summary>
        /// 根据过滤条件，获取数量
        /// </summary>
        /// <param name="whereLambda">过滤条件</param>
        /// <returns>IQueryable查询对象</returns>
        public int Count(Expression<Func<TEntity, bool>> whereLambda = null)
        {
            var query = Set().AsExpandable();
            return query.Count(whereLambda);
        }

        /// <summary>
        /// 批量更新数据，实现按需要只更新部分更新
        /// <para>如：Update(u =>u.Id==1,u =>new User{Name="ok"});</para>
        /// </summary>
        /// <param name="whereLambda">条件表达式：u =>u.Id==1</param>
        /// <param name="updateLambda">更新表达式：u =>new User{Name="ok"}</param>
        /// <returns>是否添加成功：true是，false否</returns>
        public bool BatchUpdate(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TEntity>> updateLambda)
        {
            return Set().AsQueryable().Where(whereLambda).Update(updateLambda) > -1;
        }
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns>是否添加成功：true是，false否</returns>
        public bool BatchDelete(Expression<Func<TEntity, bool>> whereLambda)
        {
            return Set().AsQueryable().Where(whereLambda).Delete() > -1;
        }
        #endregion
        #region 分页
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda">排序方式：new OrderByBuilder TEntity string (a => a.UserName[,true])，true=倒序，默认false正序</param>
        /// <returns></returns>
        public IQueryable<TEntity> Find(out int totalCount, int pageIndex = 1, int pageSize = 10, Expression<Func<TEntity, bool>> whereLambda = null, params IOrderByBuilder<TEntity>[] orderLambda)
        {
            if (pageIndex < 1) pageIndex = 1;

            var query = Set().AsExpandable();
            if (whereLambda != null)
            {
                query = query.Where(whereLambda);
            }
            if (orderLambda != null)
            {
                query = query.OrderBy(orderLambda);
            }

            totalCount = query.Count();

            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="totalCount"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize">页数大小</param>
        /// <param name="query">linq表达式</param>
        /// <returns></returns>
        public IQueryable<SEntity> Find<SEntity>(out int totalCount, int pageIndex = 1, int pageSize = 10, IQueryable<SEntity> query = null)
        {
            if (pageIndex < 1) pageIndex = 1;

            if (query == null)
            {
                totalCount = 0;
                return null;
            }

            totalCount = query.Count();

            return query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        #endregion
        #region 执行sql语句，简单封装
        /// <summary>
        /// 执行SQL返回相应实体类集合数据
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">参数</param>
        /// <returns>实体类集合数据</returns>
        public IQueryable<S> SqlQuery<S>(string SqlCommandText, params object[] paramValues)
        {
            return UnitOfWork.context.Database.SqlQuery<S>(SqlCommandText, paramValues).AsQueryable();
        }

        /// <summary>
        /// 执行SQL返回相应实体类集合数据
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">参数</param>
        /// <returns>实体类集合数据</returns>
        public IQueryable<TEntity> SqlQuery(string SqlCommandText, params object[] paramValues)
        {
            return SqlQuery<TEntity>(SqlCommandText, paramValues);
        }

        /// <summary>
        /// 执行SQL返回影响行数
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">参数</param>
        /// <returns>实体类集合数据</returns>
        public int SqlExecute(string SqlCommandText, params object[] paramValues)
        {
            return UnitOfWork.context.Database.ExecuteSqlCommand(SqlCommandText, paramValues);
        }
        /// <summary>
        /// 执行SQL返回DataSet
        /// </summary>
        /// <param name="SqlCommandText">SQL语句</param>
        /// <param name="paramValues">SQL参数</param>
        /// <returns></returns>
        public DataSet SqlQueryToDataSet(string SqlCommandText, params object[] paramValues)
        {
            SqlConnection conn = (SqlConnection)UnitOfWork.context.Database.Connection;
            SqlCommand cmd = new SqlCommand(SqlCommandText, conn);
            cmd.Parameters.AddRange(paramValues);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }
        /// <summary>
        /// 使用SqlBulkCopy批量拷贝数据（一般使用在大量数据写入，例如：一次写入1000条）
        /// </summary>
        /// <param name="entitys"></param>
        public void BulkInsertCopyAll(IEnumerable<TEntity> entitys)
        {
            if (entitys == null || entitys.Count() == 0)
            {
                return;
            }
            entitys = entitys.ToArray();
            
            string cs = UnitOfWork.context.Database.Connection.ConnectionString;
            using (var conn = new SqlConnection(cs))
            {

                Type t = typeof(TEntity);

                using (var bulkCopy = new SqlBulkCopy(conn)
                {
                    DestinationTableName = t.Name
                })
                {

                    var properties = t.GetProperties().Where(EventTypeFilter).ToArray();
                    var table = new DataTable();

                    foreach (var property in properties)
                    {
                        Type propertyType = property.PropertyType;
                        if (propertyType.IsGenericType &&
                            propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            propertyType = Nullable.GetUnderlyingType(propertyType);
                        }
                        table.Columns.Add(new DataColumn(property.Name, propertyType));
                    }
                    foreach (var entity in entitys)
                    {
                        table.Rows.Add(properties.Select(
                          property => GetPropertyValue(
                          property.GetValue(entity, null))).ToArray());
                    }

                    conn.Open();
                    bulkCopy.WriteToServer(table);
                }
            }
        }
        private bool EventTypeFilter(System.Reflection.PropertyInfo p)
        {
            var keyAttribute = Attribute.GetCustomAttribute(p,
                typeof(KeyAttribute)) as KeyAttribute;
            if (keyAttribute == null) return true;

            var generatedAttribute = Attribute.GetCustomAttribute(p,
              typeof(DatabaseGeneratedAttribute)) as DatabaseGeneratedAttribute;
            if (generatedAttribute == null) return true;
            if (generatedAttribute.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity) return true;
            
            var attribute = Attribute.GetCustomAttribute(p,
                typeof(AssociationAttribute)) as AssociationAttribute;

            if (attribute == null) return true;

            if (attribute.IsForeignKey == false) return true;

            return false;
        }

        private object GetPropertyValue(object o)
        {
            if (o == null)
                return DBNull.Value;
            return o;
        }
        #endregion
    }
}
