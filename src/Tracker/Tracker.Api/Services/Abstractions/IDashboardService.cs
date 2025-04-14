using static Shared.Dtos.Tracker.DashboardDtos;

namespace Tracker.Api.Services.Abstractions;

public interface IDashboardService
{
    Task<TotalBalanceResponse> GetTotalBalanceAsync();
}
