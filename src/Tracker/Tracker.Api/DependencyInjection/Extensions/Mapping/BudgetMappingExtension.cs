using Tracker.Api.Dtos.Budget;
using Tracker.Api.Dtos.Category;
using Tracker.Api.Entities;

namespace Tracker.Api.DependencyInjection.Extensions.Mapping;

public static class BudgetMappingExtension
{
    public static BudgetResponse ToBudgetResponse(this Budget budget)
    {
        return new BudgetResponse()
        {
            Id = budget.Id,
            CategoryId = budget.CategoryId,
            Limit = budget.Limit,
            StartDate = budget.StartDate,
            EndDate = budget.EndDate
        };
    }
}
