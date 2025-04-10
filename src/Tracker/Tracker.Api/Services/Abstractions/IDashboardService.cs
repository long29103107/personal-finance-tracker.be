using Tracker.Api.Dtos.Dashboard;

namespace Tracker.Api.Services.Abstractions;

public interface IDashboardService
{
    Task<TotalBalanceResponse> GetTotalBalanceAsync();
}
