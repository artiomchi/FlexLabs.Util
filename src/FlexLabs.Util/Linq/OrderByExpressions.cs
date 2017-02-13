using System;
using System.Linq;
using System.Linq.Expressions;

namespace FlexLabs.Linq
{
    // Derived from http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2641510&SiteID=1
    public class OrderByExpressions<TKey, TResult1, TResult2, TResult3, TResult4, TResult5> : IOrderByExpression<TKey>
    {
        private Expression<Func<TKey, TResult1>> expr1 = null;
        private Expression<Func<TKey, TResult2>> expr2 = null;
        private Expression<Func<TKey, TResult3>> expr3 = null;
        private Expression<Func<TKey, TResult4>> expr4 = null;
        private Expression<Func<TKey, TResult5>> expr5 = null;
        public OrderByExpressions(
            Expression<Func<TKey, TResult1>> expression1,
            Expression<Func<TKey, TResult2>> expression2 = null,
            Expression<Func<TKey, TResult3>> expression3 = null,
            Expression<Func<TKey, TResult4>> expression4 = null,
            Expression<Func<TKey, TResult5>> expression5 = null)
        {
            expr1 = expression1;
            expr2 = expression2;
            expr3 = expression3;
            expr4 = expression4;
            expr5 = expression5;
        }
        public IOrderedQueryable<TKey> ApplyOrdering(IQueryable<TKey> query) => ApplyOrdering(query, true);

        public IOrderedQueryable<TKey> ApplyOrdering(IQueryable<TKey> query, bool ascending)
        {
            if (ascending)
            {
                var result = query.OrderBy(expr1);
                if (expr2 != null)
                    result = result.ThenBy(expr2);
                if (expr3 != null)
                    result = result.ThenBy(expr3);
                if (expr4 != null)
                    result = result.ThenBy(expr4);
                if (expr5 != null)
                    result = result.ThenBy(expr5);

                return result;
            }
            else
            {
                var result = query.OrderByDescending(expr1);
                if (expr2 != null)
                    result = result.ThenByDescending(expr2);
                if (expr3 != null)
                    result = result.ThenByDescending(expr3);
                if (expr4 != null)
                    result = result.ThenByDescending(expr4);
                if (expr5 != null)
                    result = result.ThenByDescending(expr5);

                return result;
            }
        }
    }
}
