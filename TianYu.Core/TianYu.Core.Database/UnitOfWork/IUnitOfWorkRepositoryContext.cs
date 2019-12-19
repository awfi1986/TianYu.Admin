using System;

namespace TianYu.Core.Common.UnitOfWork
{
    /// <summary>
    /// 仓储上下文工作单元接口，使用这个的一般情况是多个仓储之间存在事务性的操作，用于标记聚合根的增删改状态
    /// </summary>
    public interface IUnitOfWorkRepositoryContext:IUnitOfWork,IDisposable
    {
        /// <summary>
        /// 将集合根的状态标记为新建,但EF上下文未提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        void RegisterNew<TEntity>(TEntity obj) where TEntity : BaseAggregateRoot;

        /// <summary>
        /// 将聚合根的状态标记为修改，但EF上下文此时并未提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fields"></param>
        void RegisterModified<TEntity>(TEntity obj, params string[] fields) where TEntity : BaseAggregateRoot;

        /// <summary>
        /// 将聚合根的状态标记为删除，但EF上下文此时并未提交
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        void RegisterDeleted<TEntity>(TEntity obj) where TEntity : BaseAggregateRoot;
    }
}
