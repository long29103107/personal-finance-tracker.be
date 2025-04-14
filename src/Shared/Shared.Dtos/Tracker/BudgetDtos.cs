using Shared.Dtos.Abstractions;

namespace Shared.Dtos.Tracker;

public static class BudgetDtos
{
    //Request
    public sealed class BudgetCreateRequest : Request
    {
        public int CategoryId { get; set; }
        public decimal Limit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public sealed class BudgetUpdateRequest : Request
    {
        public int CategoryId { get; set; }
        public decimal Limit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }


    //Response
    public sealed class BudgetListResponse : Response
    {
        public string Email { get; set; }
        public int? ParentCategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = "Expense";
    }

    public sealed class BudgetResponse : Response
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public decimal Limit { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
