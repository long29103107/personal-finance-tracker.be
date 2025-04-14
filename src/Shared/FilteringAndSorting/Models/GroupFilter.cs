using System.Linq.Expressions;

namespace FilteringAndSortingExpression.Models;
public sealed class GroupFilter : BaseFilter
{
    public Expression Expression { get; set; } = null;
}