using Shared.Domain;

namespace Tracker.Api.Dtos.Transaction;

public sealed class TransactionDashboardResponse : Response
{
    public List<MonthlySummaryReponse> MonthlySummaries { get; set; } = new();
    public List<TopCategoryResponse> TopCategories { get; set; } = new();
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