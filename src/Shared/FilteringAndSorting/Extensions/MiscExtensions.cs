namespace FilteringAndSortingExpression.Extensions;

public static class MiscExtensions
{
    public static bool ContainsIgnoreCase(this string source, string toCheck)
    {
        return source?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public static bool StartsWithIgnoreCase(this string source, string toCheck)
    {
        return source?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) == 0;
    }

    public static bool EndsWithIgnoreCase(this string source, string toCheck)
    {
        var x = source.Length - toCheck.Length;
        return source?.IndexOf(toCheck, StringComparison.OrdinalIgnoreCase) == x;
    }
}