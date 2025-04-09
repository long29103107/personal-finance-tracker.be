using Tracker.Api.Dtos.Goal;
using Tracker.Api.Dtos.Category;
using Tracker.Api.Entities;

namespace Tracker.Api.DependencyInjection.Extensions.Mapping;

public static class GoalMappingExtension
{
    public static GoalResponse ToGoalResponse(this Goal goal)
    {
        return new GoalResponse()
        {
            Id = goal.Id,
            Name = goal.Name,
            TargetAmount = goal.TargetAmount,
            CurrentAmount = goal.CurrentAmount,
            Deadline = goal.Deadline
        };
    }
}
