using Shared.Domain.Abstractions;

namespace Tracker.Api.Entities;

public class Goal : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal TargetAmount { get; set; }
    public decimal CurrentAmount { get; set; } = 0;
    public DateTime Deadline { get; set; }
}
