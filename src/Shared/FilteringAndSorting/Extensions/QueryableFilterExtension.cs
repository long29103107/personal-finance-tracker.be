using Shared.Dtos.Abstractions;

namespace FilteringAndSortingExpression.Extensions;

public static class QueryableFilterExtension
{
    private static FilterService _filterService = new FilterService();

    public static IQueryable<T> Filter<T>(this IQueryable<T> queryable, ListRequest request)
    {
        var filter = _filterService.Filter<T>(request);

        if(filter == null)
        {
            return queryable;
        }

        return queryable.Where(filter);
    }
}
