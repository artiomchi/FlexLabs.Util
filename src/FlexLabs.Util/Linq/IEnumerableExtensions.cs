using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FlexLabs.Linq
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Conditional where extension. Will only apply the predicate to the collection if the condition is true
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="items">Item collection</param>
        /// <param name="condition">Indicates whether the filter should be applied</param>
        /// <param name="predicate">Filter to be applied</param>
        /// <returns>Filtered collection if condition is true, otherwise returns the original collection</returns>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> items, bool condition, Func<T, bool> predicate)
            => condition
                ? items.Where(predicate)
                : items;

        /// <summary>
        /// Conditional where extension. Will only apply the predicate to the collection if the condition is true
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="items">Item collection</param>
        /// <param name="condition">Indicates whether the filter should be applied</param>
        /// <param name="predicate">Filter to be applied</param>
        /// <returns>Filtered collection if condition is true, otherwise returns the original collection</returns>
        public static IEnumerable<T> Where<T>(this IEnumerable<T> items, bool condition, Func<T, int, bool> predicate)
            => condition
                ? items.Where(predicate)
                : items;

        /// <summary>
        /// Conditional where extension. Will only apply the predicate to the query if the condition is true
        /// </summary>
        /// <typeparam name="T">Item type</typeparam>
        /// <param name="query">Query to be processed</param>
        /// <param name="condition">Indicates whether the filter should be applied</param>
        /// <param name="predicate">Filter to be applied</param>
        /// <returns>Filtered query if condition is true, otherwise returns the original query</returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
            => condition
                ? query.Where(predicate)
                : query;
    }
}
