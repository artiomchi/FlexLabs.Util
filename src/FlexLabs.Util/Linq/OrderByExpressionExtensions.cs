using System.Collections.Generic;
using System.Linq;

namespace FlexLabs.Linq
{
    public static class OrderByExpressionExtensions
    {
        public static IOrderedQueryable<TModel> OrderBy<TModel, TKey>(this IQueryable<TModel> source, OrderByExpressionCollection<TKey, TModel> sorter, TKey sortBy, bool sortAsc)
        {
            if (!sorter.ContainsKey(sortBy))
                throw new KeyNotFoundException($"The given key {sortBy} was not present in the sorter collection");

            return sorter[sortBy].ApplyOrdering(source, sortAsc);
        }
    }
}
