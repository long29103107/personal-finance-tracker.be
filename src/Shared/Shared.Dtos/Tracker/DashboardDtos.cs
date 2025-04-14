using Shared.Dtos.Abstractions;

namespace Shared.Dtos.Tracker;
public static class DashboardDtos
{
    //Requestsss

    //Response

    public sealed class TotalBalanceResponse : Response
    {
        public decimal TotalBalance { get; set; }
    }
}