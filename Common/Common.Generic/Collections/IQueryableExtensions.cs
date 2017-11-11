using System.Linq;

namespace Common.Generic.Collections
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> GetSelfOrNewIfNull<T>(this IQueryable<T> queryable)
        {
            return queryable
                ?? Enumerable.Empty<T>().AsQueryable();
        }
    }
}
