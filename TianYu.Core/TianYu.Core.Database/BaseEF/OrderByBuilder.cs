using System;
using System.Linq;
using System.Linq.Expressions;

namespace TianYu.Core.Common.BaseEF
{
    public class OrderByBuilder<TEntity, TOrderBy> : IOrderByBuilder<TEntity> where TEntity : class
    {
        private Expression<Func<TEntity, TOrderBy>> _expression;
        private bool _descending;

        public OrderByBuilder(Expression<Func<TEntity, TOrderBy>> expression, bool descending = false)
        {
            _expression = expression;
            _descending = descending;
        }

        public IOrderedQueryable<TEntity> OrderBy(IQueryable<TEntity> query)
        {
            if (_descending)
                return query.OrderByDescending(_expression);
            else
                return query.OrderBy(_expression);
        }

        public IOrderedQueryable<TEntity> ThenBy(IOrderedQueryable<TEntity> query)
        {
            if (_descending)
                return query.ThenByDescending(_expression);
            else
                return query.ThenBy(_expression);
        }
    }
}
