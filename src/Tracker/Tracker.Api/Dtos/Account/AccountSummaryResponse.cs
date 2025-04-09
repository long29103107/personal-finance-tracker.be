using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Account;

public sealed class AccountSummaryResponse : Response
{
    public List<AccountSummaryDto> Accounts { get; set; }
    public decimal TotalBalance
    {
        get
        {
            return this.Accounts.Sum(a => a.Balance);
        }
    }
}


public class AccountSummaryDto
{
    public string AccountName { get; set; }
    public decimal Balance { get; set; }
}
