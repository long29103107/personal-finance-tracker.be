using Tracker.Api.Constants;

namespace Tracker.Api.Entities;

public class Category : BaseEntity
{
    public string UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = TransactionTypeConstants.Expense; 

    public List<Transaction> Transactions { get; set; } = new();
}
