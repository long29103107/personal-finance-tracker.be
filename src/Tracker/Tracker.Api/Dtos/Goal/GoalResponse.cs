using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Goal;

public sealed class GoalResponse : Response
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; } = 0;
    public DateTime Deadline { get; set; }
}
