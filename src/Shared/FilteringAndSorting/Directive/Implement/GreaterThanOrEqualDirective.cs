using System.Linq.Expressions;

namespace FilteringAndSortingExpression.Directive.Implement
{
    internal class GreaterThanOrEqualDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return Constants.ComparisonOperator.GreaterThanAndEqual;
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            return Expression.GreaterThanOrEqual(property, value);
        }
    }
}
