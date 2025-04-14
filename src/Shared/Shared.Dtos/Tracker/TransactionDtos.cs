
using Shared.Dtos.Abstractions;

namespace Shared.Dtos.Tracker;

public static class TransactionDtos
{
    private const string _expense = "Expense";

    //Request
    public sealed class TransactionCreateRequest : Request
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = _expense;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
    }


    //Response
    public sealed class TransactionDashboardResponse : Response
    {
        public List<MonthlySummaryReponse> MonthlySummaries { get; set; } = new();
        public List<TopCategoryResponse> TopCategories { get; set; } = new();
    }

    public sealed class TransactionListResponse : Response
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = _expense;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
    }

    public sealed class TransactionResponse : Response
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = _expense;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
    }

    public sealed class TransactionUpdateRequest : Request
    {
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } = _expense;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string? Description { get; set; }
    }


}


public class MonthlySummaryReponse
{
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal TotalIncome { get; set; }
    public decimal TotalExpense { get; set; }
}

public class TopCategoryResponse
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal TotalAmount { get; set; }
}
