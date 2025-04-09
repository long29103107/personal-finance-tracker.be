using Shared.Domain.Abstractions;
using Tracker.Api.Constants;

namespace Tracker.Api.Entities;

public class Account : BaseEntity
{
    public string Email { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public string Currency { get; set; } = CurrencyConstants.VND;
    public List<Transaction> Transactions { get; set; } = new();
}
