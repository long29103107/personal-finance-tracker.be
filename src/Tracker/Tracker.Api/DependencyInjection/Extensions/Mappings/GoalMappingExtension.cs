using Tracker.Api.Entities;
using static Shared.Dtos.Tracker.GoalDtos;

namespace Tracker.Api.DependencyInjection.Extensions.Mappings;

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
