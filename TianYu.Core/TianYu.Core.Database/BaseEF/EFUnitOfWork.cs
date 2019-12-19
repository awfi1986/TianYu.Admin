using TianYu.Core.Common.UnitOfWork;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace TianYu.Core.Common.BaseEF
{
    public class EFUnitOfWork : IEFUnitOfWork
    {

        /// <summary>
        /// 通过工作单元向外暴露的EF上下文对象
        /// </summary>
        public DbContext context { get; }

        public EFUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        #region IUnitOfWork接口
        public bool isCommitted
        {
            get;
            set;
        }

        public int SaveChanges()
        {
            if (isCommitted)
            {
                return 0;
            }
            try
            {
                int result = context.SaveChanges();

                isCommitted = true;
                return result;
            }
            catch (DbUpdateException ex)
            {
                //处理成不跟踪
                isCommitted = false;
                foreach(var entry in ex.Entries)
                {
                    entry.State = EntityState.Detached;
                }

                throw ex;
            }
        }
        public Task<int> SaveChangesAsync()
        {
            if (isCommitted)
            {
                return new Task<int>(()=>{ return 0; });
            }
            try
            {
                var result = context.SaveChangesAsync();

                isCommitted = true;
                return result;
            }
            catch (DbUpdateException ex)
            {
                //处理成不跟踪
                isCommitted = false;
                foreach (var entry in ex.Entries)
                {
                    entry.State = EntityState.Detached;
                }

                throw ex;
            }
        }
        #endregion

        #region IDisposable接口
        public void Dispose()
        {
            if (!isCommitted) {
                SaveChanges();
            }
            context.Dispose();
        }
        #endregion

        #region IUnitOfWorkRepositoryContext接口
        public void RegisterDeleted<TEntity>(TEntity obj) where TEntity : BaseAggregateRoot
        {
            context.Entry(obj).State = EntityState.Deleted;
            isCommitted = false;
        }

        public void RegisterModified<TEntity>(TEntity obj, params string[] fields) where TEntity : BaseAggregateRoot
        {
            if (context.Entry(obj).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(obj);
            }
            if (fields != null && fields.Length > 0)
            {
                var setEntry = ((IObjectContextAdapter)context).ObjectContext.ObjectStateManager.GetObjectStateEntry(obj);
                foreach (var field in fields)
                {
                    if (!string.IsNullOrEmpty(field))
                    {
                        setEntry.SetModifiedProperty(field);
                    }
                }
            }
            else
            {
                context.Entry(obj).State = EntityState.Modified;
            }

            isCommitted = false;
        }

        public void RegisterNew<TEntity>(TEntity obj) where TEntity : BaseAggregateRoot
        {
            var state = context.Entry(obj).State;
            if (state == EntityState.Detached)
            {
                context.Entry(obj).State = EntityState.Added;
            }
            isCommitted = false;
        }
        
        #endregion
    }
}
