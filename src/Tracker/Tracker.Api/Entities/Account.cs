using System.Transactions;

namespace Tracker.Api.Entities;

public class Account : BaseEntity
{
    public string UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public string Currency { get; set; } = "VND";
    public List<Transaction> Transactions { get; set; } = new();
}
