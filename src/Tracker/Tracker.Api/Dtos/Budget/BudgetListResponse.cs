using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Budget;

public sealed class BudgetListResponse : Response
{
    public string Email { get; set; }
    public int? ParentCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = TransactionTypeConstants.Expense;
}
