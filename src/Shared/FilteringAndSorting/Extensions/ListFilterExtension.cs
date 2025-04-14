using Shared.Dtos.Abstractions;

namespace FilteringAndSortingExpression.Extensions;

public static class ListFilterExtension
{
    private static FilterService _filterService = new FilterService();

    /// <summary>
    /// Filter in list
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="fe"></param>
    /// <returns></returns>
    public static List<T> Filter<T>(this List<T> list, ListRequest request)
    {
        var filter = _filterService.Filter<T>(request);

        if(filter != null)
        {
            return list.Where(filter.Compile()).ToList();
        }

        return list.ToList();
    }

    /// <summary>
    /// Determines whether the collection is null or contains no elements.
    /// </summary>
    /// <typeparam name="T">The IEnumerable type.</typeparam>
    /// <param name="enumerable">The enumerable, which may be null or empty.</param>
    /// <returns>
    ///     <c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable == null)
        {
            return true;
        }
        /* If this is a list, use the Count property for efficiency. 
         * The Count property is O(1) while IEnumerable.Count() is O(N). */
        var collection = enumerable as ICollection<T>;
        if (collection != null)
        {
            return collection.Count < 1;
        }
        return !enumerable.Any();
    }
}
