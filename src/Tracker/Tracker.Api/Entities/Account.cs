using Shared.Domain.Abstractions;

namespace Tracker.Api.Entities;

public class Account : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public string Currency { get; set; } = "VND";
    public List<Transaction> Transactions { get; set; } = new();
}
