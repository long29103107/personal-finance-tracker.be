using Shared.Domain.Abstractions;
using Tracker.Api.Constants;

namespace Tracker.Api.Entities;

public class Transaction : BaseEntity
{
    public string Email { get; set; }
    public int AccountId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = TransactionTypeConstants.Expense;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
    public Account Account { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
