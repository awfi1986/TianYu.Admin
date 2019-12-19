using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianYu.Core.Common.BaseEF
{
    /// <summary>
    /// 排序扩展
    /// </summary>
    public static class OrderByExpression
    {
        /// <summary>
        /// 多字段排序扩展方法
        /// </summary>
        /// <param name="query"></param>
        /// <param name="orderByExpressions">高级排序参数</param>
        /// <returns></returns>
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> query, params IOrderByBuilder<TEntity>[] orderByExpressions) where TEntity : class
        {
            if (orderByExpressions == null)
                return query;

            IOrderedQueryable<TEntity> output = null;

            foreach (var orderByExpression in orderByExpressions)
            {
                if (output == null)
                    output = orderByExpression.OrderBy(query);
                else
                    output = orderByExpression.ThenBy(output);
            }

            return output ?? query;
        }

    }
}
