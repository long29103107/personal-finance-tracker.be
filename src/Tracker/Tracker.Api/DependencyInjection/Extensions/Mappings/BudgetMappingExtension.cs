using static Shared.Dtos.Tracker.BudgetDtos;
using Tracker.Api.Entities;

namespace Tracker.Api.DependencyInjection.Extensions.Mappings;

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
