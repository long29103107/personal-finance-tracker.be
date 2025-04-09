using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Transaction;

public sealed class TransactionUpdateRequest : Request
{
    public int AccountId { get; set; }
    public int CategoryId { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = TransactionTypeConstants.Expense;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public string? Description { get; set; }
}
