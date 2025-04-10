using Shared.Domain;

namespace Tracker.Api.Dtos.Dashboard;

public class TotalBalanceResponse : Response
{
    public decimal TotalBalance { get; set; }
}
