using System.Linq.Expressions;
using System.Reflection;

namespace FilteringAndSortingExpression.Extensions;

public static partial class LinqExtensions
{
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