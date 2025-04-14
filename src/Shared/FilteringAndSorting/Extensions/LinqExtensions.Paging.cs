using Microsoft.EntityFrameworkCore;

namespace FilteringAndSortingExpression.Extensions;

public static partial class LinqExtensions
{
    #region Paging
    public static IQueryable<T> Paging<T>(this IQueryable<T> query, int page, int pageSize) where T : class
    {
        var skip = (page - 1) * pageSize;
        return query.Skip(skip).Take(pageSize);
    }

    public static List<T> Paging<T>(this List<T> list, int page, int pageSize) where T : class
    {
        var skip = (page - 1) * pageSize;
        return list.Skip(skip).Take(pageSize).ToList();
    }
    #endregion

    #region To Page

    public static async Task<List<T>> ToPageAsync<T>(this IQueryable<T> query, string orderBy, bool orderDesc = false, int page = 1, int pageSize = 50) where T : class
    {
        return await query.OrderBy(orderBy, orderDesc).Paging(page, pageSize).ToListAsync();
    }

    public static async Task<List<T>> ToPageAsync<T>(this IQueryable<T> query, int page = 1, int pageSize = 50) where T : class
    {
        return await query.Paging(page, pageSize).ToListAsync();
    }

    public static List<T> ToPage<T>(this List<T> query, string orderBy, bool orderDesc = false, int page = 1, int pageSize = 50) where T : class
    {
        return query.OrderBy(orderBy, orderDesc).Paging(page, pageSize).ToList();
    }

    public static List<T> ToPage<T>(this IQueryable<T> query, string orderBy, bool orderDesc = false, int page = 1, int pageSize = 50) where T : class
    {
        return query.OrderBy(orderBy, orderDesc).Paging(page, pageSize).ToList();
    }

    public static List<T> ToPage<T>(this List<T> query, int page = 1, int pageSize = 50) where T : class
    {
        return query.Paging(page, pageSize).ToList();
    }
    #endregion
}