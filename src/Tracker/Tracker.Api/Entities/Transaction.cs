using Tracker.Api.Constants;

namespace Tracker.Api.Entities;

public class Transaction : BaseEntity
{
    public string UserId { get; set; }
    public string AccountId { get; set; }
    public string CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = TransactionTypeConstants.Expense;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public Account Account { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
