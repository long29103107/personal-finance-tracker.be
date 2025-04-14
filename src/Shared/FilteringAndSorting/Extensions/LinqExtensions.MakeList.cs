using Microsoft.EntityFrameworkCore;
using Shared.Dtos.Abstractions;

namespace FilteringAndSortingExpression.Extensions;

public static partial class LinqExtensions
{
    #region Sync
    public static List<T> MakeList<T>(this ListRequest dto, List<T> queryset) where T : class
    {
        System.Reflection.PropertyInfo prop = typeof(T).GetProperties()
            .FirstOrDefault(x => x.Name.ToLower().Equals(dto.OrderBy.ToLower()));

        if (dto.OrderDesc)
        {
            return queryset.OrderByDescending(x => prop.GetValue(x, null)).ToList();
        }

        return queryset.OrderBy(x => prop.GetValue(x, null)).ToList();
    }

    public static List<T> MakeList<T>(this ListRequest dto, IQueryable<T> queryset) where T : class
    {
        return queryset.OrderBy(dto.OrderBy, dto.OrderDesc).ToList();
    }

    public static List<T> MakeList<T>(this PagingListRequest dto, List<T> queryset) where T : class
    {
        dto.Count = queryset.Count();
        if (dto.Count > 0)
        {
            return queryset.ToPage(dto.OrderBy, dto.OrderDesc, dto.Page, dto.PageSize);
        }
        else
        {
            return null;
        }
    }

    public static List<T> MakeList<T>(this PagingListRequest dto, IQueryable<T> queryset) where T : class
    {
        dto.Count = queryset.Count();
        if (dto.Count > 0)
        {
            return queryset.ToPage(dto.OrderBy, dto.OrderDesc, dto.Page, dto.PageSize);
        }
        else
        {
            return null;
        }
    }
    #endregion

    #region Async
    public static async Task<List<T>> MakeListAsync<T>(this ListRequest dto, IQueryable<T> queryset) where T : class
    {
        return await queryset.OrderBy(dto.OrderBy, dto.OrderDesc).ToListAsync();
    }

    public static async Task<List<T>> MakeListAsync<T>(this PagingListRequest dto, IQueryable<T> queryset) where T : class
    {
        dto.Count = await queryset.CountAsync();
        if (dto.Count > 0)
        {
            return await queryset.ToPageAsync(dto.OrderBy, dto.OrderDesc, dto.Page, dto.PageSize);
        }
        else
        {
            return null;
        }
    }
    #endregion
}