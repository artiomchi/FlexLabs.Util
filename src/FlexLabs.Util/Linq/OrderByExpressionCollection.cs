using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FlexLabs.Linq
{
    public class OrderByExpressionCollection<TKey, TModel> : IEnumerable
    {
        private Dictionary<TKey, IOrderByExpression<TModel>> storage = new Dictionary<TKey, IOrderByExpression<TModel>>();

        public void Add<TResult>(TKey key, Expression<Func<TModel, TResult>> expression)
        {
            storage.Add(key, new OrderByExpression<TModel, TResult>(expression));
        }

        public void Add<TResult1, TResult2>(TKey key, 
            Expression<Func<TModel, TResult1>> expression1,
            Expression<Func<TModel, TResult2>> expression2)
        {
            storage.Add(key, new OrderByExpressions<TModel, TResult1, TResult2, byte, byte, byte>(expression1, expression2));
        }

        public void Add<TResult1, TResult2, TResult3>(TKey key,
            Expression<Func<TModel, TResult1>> expression1,
            Expression<Func<TModel, TResult2>> expression2,
            Expression<Func<TModel, TResult3>> expression3)
        {
            storage.Add(key, new OrderByExpressions<TModel, TResult1, TResult2, TResult3, byte, byte>(expression1, expression2, expression3));
        }

        public void Add<TResult1, TResult2, TResult3, TResult4>(TKey key,
            Expression<Func<TModel, TResult1>> expression1,
            Expression<Func<TModel, TResult2>> expression2,
            Expression<Func<TModel, TResult3>> expression3,
            Expression<Func<TModel, TResult4>> expression4)
        {
            storage.Add(key, new OrderByExpressions<TModel, TResult1, TResult2, TResult3, TResult4, byte>(expression1, expression2, expression3, expression4));
        }

        public void Add<TResult1, TResult2, TResult3, TResult4, TResult5>(TKey key,
            Expression<Func<TModel, TResult1>> expression1,
            Expression<Func<TModel, TResult2>> expression2,
            Expression<Func<TModel, TResult3>> expression3,
            Expression<Func<TModel, TResult4>> expression4,
            Expression<Func<TModel, TResult5>> expression5)
        {
            storage.Add(key, new OrderByExpressions<TModel, TResult1, TResult2, TResult3, TResult4, TResult5>(expression1, expression2, expression3, expression4, expression5));
        }

        public IOrderByExpression<TModel> this[TKey key] => storage[key];

        public Boolean ContainsKey(TKey key) => storage.ContainsKey(key);

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
