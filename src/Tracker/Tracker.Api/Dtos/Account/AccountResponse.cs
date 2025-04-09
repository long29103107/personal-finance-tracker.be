using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Account;

public sealed class AccountResponse : Response
{
    public int Id { get; set; } 
    public int? UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 0;
    public string Currency { get; set; } = CurrencyConstants.VND;
}
