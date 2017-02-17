using System;
using System.Linq;
using System.Linq.Expressions;

namespace FlexLabs.Linq
{
    // Derived from http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2641510&SiteID=1
    public class OrderByExpression<TKey, TResult> : IOrderByExpression<TKey>
    {
        private Expression<Func<TKey, TResult>> _exp = null;
        public OrderByExpression(Expression<Func<TKey, TResult>> expression)
        {
            _exp = expression;
        }

        public IOrderedQueryable<TKey> ApplyOrdering(IQueryable<TKey> query) => query.OrderBy(_exp);

        public IOrderedQueryable<TKey> ApplyOrdering(IQueryable<TKey> query, bool ascending)
            => ascending
                ? query.OrderBy(_exp)
                : query.OrderByDescending(_exp);
    }
}
