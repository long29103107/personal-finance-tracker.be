using FilteringAndSortingExpression.Extensions;
using System.Linq.Expressions;
using System.Reflection;

namespace FilteringAndSortingExpression.Directive.Implement
{
    internal class EndsWithIgnoreCaseDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return Constants.ComparisonOperator.EndsWithIgnoreCase;
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            var function = typeof(MiscExtensions).GetMethod(nameof(MiscExtensions.EndsWithIgnoreCase),
                    BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null,
                    new[] { typeof(string), typeof(string) }, null);

            if (value.Type != typeof(string))
                throw new ArgumentException("Value must be string type");

            if (function == null)
                throw new Exception($"{nameof(MiscExtensions.EndsWithIgnoreCase)} function is not found");

            return Expression.Call(property, function, value);
        }
    }
}
