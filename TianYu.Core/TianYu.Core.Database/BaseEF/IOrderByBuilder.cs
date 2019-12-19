using System.Linq;

namespace TianYu.Core.Common.BaseEF
{
    public interface IOrderByBuilder<TEntity> where TEntity : class
    {
        IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> query);
        IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> query);
    }
}
