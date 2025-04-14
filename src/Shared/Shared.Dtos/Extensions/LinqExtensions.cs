using Shared.Dtos.Abstractions;
using System.Linq.Expressions;
using System.Reflection;

namespace Shared.Dtos.Extensions;

public static partial class LinqExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, ListRequest dto) where T : class
    {
        return query.OrderBy(dto.OrderBy, dto.OrderDesc);
    }

    public static List<string> GetPropertiesAsString<T>()
    {
        IList<PropertyInfo> props = new List<PropertyInfo>(typeof(T).GetProperties());
        return props.Select(prop => prop.Name.ToLower()).ToList();
    }

    public static string GetPropertiesDefaultSortAsString<T>(string defaultSort = "id")
    {
        IList<PropertyInfo> props = new List<PropertyInfo>(typeof(T).GetProperties());

        if (props.Any(x => x.Name.ToLower() == defaultSort))
        {
            return defaultSort;
        }

        return props.FirstOrDefault()?.Name.ToLower() ?? string.Empty;
    }

    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source)
        where T : class
    {
        if (source.IsNullOrEmpty())
        {
            return source;
        }

        return source.Where(x => x != null);
    }

    public static IQueryable<T> WhereNotNull<T>(this IQueryable<T> source)
        where T : class
    {
        if (source == null)
        {
            return source;
        }

        return source.Where(x => x != null);
    }

    private static IOrderedQueryable<T> ApplyOrder<T>(
           IQueryable<T> source,
           string property,
           string methodName)
    {
        string[] props = property.Split('.');
        Type type = typeof(T);
        ParameterExpression arg = Expression.Parameter(type, "x");
        Expression expr = arg;
        foreach (string prop in props)
        {
            // use reflection (not ComponentModel) to mirror LINQ
            PropertyInfo pi = type.GetProperty(prop, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (pi == null)
            {
                throw new Exception("Invalid sorting (" + prop + ")");
            }
            expr = Expression.Property(expr, pi);
            type = pi.PropertyType;
        }
        Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
        LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

        object result = typeof(Queryable).GetMethods().Single(
                method => method.Name == methodName
                        && method.IsGenericMethodDefinition
                        && method.GetGenericArguments().Length == 2
                        && method.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), type)
                .Invoke(null, new object[] { source, lambda });
        return (IOrderedQueryable<T>)result;
    }

    public static IQueryable<T> OrderBy<T>(
          this IQueryable<T> source,
          string property, bool isDesc = false)
    {
        if (property == null)
        {
            return source;
        }
        if (isDesc)
        {
            return ApplyOrder<T>(source, property, "OrderByDescending");
        }
        return ApplyOrder<T>(source, property, "OrderBy");
    }

    public static List<T> OrderBy<T>(
      this List<T> source,
      string property, bool isDesc = false)
    {
        if (property == null)
        {
            return source;
        }

        System.Reflection.PropertyInfo prop = typeof(T).GetProperties().Where(x => x.Name.ToLower().Equals(property.ToLower())).FirstOrDefault();

        if (isDesc)
        {
            return source.OrderByDescending(x => prop.GetValue(x, null)).ToList();
        }
        return source.OrderByDescending(x => prop.GetValue(x, null)).ToList();
    }

    public static IOrderedQueryable<T> ThenBy<T>(
        this IOrderedQueryable<T> source,
        string property, bool isDesc = false)
    {
        if (property == null)
        {
            return source;
        }

        if (isDesc)
        {
            return ApplyOrder<T>(source, property, "ThenByDescending");
        }

        return ApplyOrder<T>(source, property, "ThenBy");
    }
}