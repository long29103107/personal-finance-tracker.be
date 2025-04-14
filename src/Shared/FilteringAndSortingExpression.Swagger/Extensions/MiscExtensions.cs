namespace FilteringAndSortingExpression.Swagger.Extensions;

public static class MiscExtensions
{
    public static string LowerFirstChar(this string str)
    {
        if (str != null)
        {
            return Char.ToLowerInvariant(str[0]) + str.Substring(1);
        }
        return str;
    }
}