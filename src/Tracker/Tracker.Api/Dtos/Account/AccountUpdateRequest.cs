using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Account;

public sealed class AccountUpdateRequest : Request
{
    public string Email { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public string Currency { get; set; } = CurrencyConstants.VND;
}
