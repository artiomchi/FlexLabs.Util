using System.Linq;

namespace FlexLabs.Linq
{
    public interface IOrderByExpression<T>
    {
        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query);
        IOrderedQueryable<T> ApplyOrdering(IQueryable<T> query, bool ascending);
    }
}
