
using Shared.Dtos.Abstractions;

namespace Shared.Dtos.Tracker;

public static class GoalDtos
{
    //Request
    public sealed class GoalUpdateRequest : Request
    {
        public string Name { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; } = 0;
        public DateTime Deadline { get; set; }
    }

    public sealed class GoalCreateRequest : Request
    {
        public string Name { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; } = 0;
        public DateTime Deadline { get; set; }
    }


    //Response
    public sealed class GoalResponse : Response
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; } = 0;
        public DateTime Deadline { get; set; }
    }

    public sealed class GoalListResponse : Response
    {
        public string Name { get; set; } = string.Empty;
        public decimal TargetAmount { get; set; }
        public decimal CurrentAmount { get; set; } = 0;
        public DateTime Deadline { get; set; }
    }

}
