using System;
using System.Linq;
using System.Linq.Expressions;

namespace FlexLabs.Linq
{
    // Derived from http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=2641510&SiteID=1
    public class OrderByExpressions<TKey, TResult1, TResult2, TResult3, TResult4, TResult5> : IOrderByExpression<TKey>
    {
        private Expression<Func<TKey, TResult1>> _expr1 = null;
        private Expression<Func<TKey, TResult2>> _expr2 = null;
        private Expression<Func<TKey, TResult3>> _expr3 = null;
        private Expression<Func<TKey, TResult4>> _expr4 = null;
        private Expression<Func<TKey, TResult5>> _expr5 = null;
        public OrderByExpressions(
            Expression<Func<TKey, TResult1>> expression1,
            Expression<Func<TKey, TResult2>> expression2 = null,
            Expression<Func<TKey, TResult3>> expression3 = null,
            Expression<Func<TKey, TResult4>> expression4 = null,
            Expression<Func<TKey, TResult5>> expression5 = null)
        {
            _expr1 = expression1;
            _expr2 = expression2;
            _expr3 = expression3;
            _expr4 = expression4;
            _expr5 = expression5;
        }
        public IOrderedQueryable<TKey> ApplyOrdering(IQueryable<TKey> query) => ApplyOrdering(query, true);

        public IOrderedQueryable<TKey> ApplyOrdering(IQueryable<TKey> query, bool ascending)
        {
            if (ascending)
            {
                var result = query.OrderBy(_expr1);
                if (_expr2 != null)
                    result = result.ThenBy(_expr2);
                if (_expr3 != null)
                    result = result.ThenBy(_expr3);
                if (_expr4 != null)
                    result = result.ThenBy(_expr4);
                if (_expr5 != null)
                    result = result.ThenBy(_expr5);

                return result;
            }
            else
            {
                var result = query.OrderByDescending(_expr1);
                if (_expr2 != null)
                    result = result.ThenByDescending(_expr2);
                if (_expr3 != null)
                    result = result.ThenByDescending(_expr3);
                if (_expr4 != null)
                    result = result.ThenByDescending(_expr4);
                if (_expr5 != null)
                    result = result.ThenByDescending(_expr5);

                return result;
            }
        }
    }
}
