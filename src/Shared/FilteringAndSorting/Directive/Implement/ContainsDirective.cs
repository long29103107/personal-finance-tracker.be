﻿using System.Linq.Expressions;
using FilteringAndSortingExpression.Constants;

namespace FilteringAndSortingExpression.Directive.Implement
{
    internal class ContainsDirective : IFilterDirective
    {
        public string FilterSyntax
        {
            get
            {
                return Constants.ComparisonOperator.Contains;
            }
        }

        public Expression GenerateExpression(ref MemberExpression property, ConstantExpression value)
        {
            var function = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

            if (value.Type != typeof(string))
                throw new ArgumentException("Value must be string type");

            if (function == null)
                throw new Exception("Contains function is not found");

            return Expression.Call(property, function, value);
        }
    }
}
