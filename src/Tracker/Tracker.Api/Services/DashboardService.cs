using Microsoft.EntityFrameworkCore;
using Tracker.Api.Dtos.Dashboard;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Services;

public class DashboardService(IAccountRepository _accountRepo) : IDashboardService
{
    public async Task<TotalBalanceResponse> GetTotalBalanceAsync()
    {
        var total = await _accountRepo.FindAll().Select(x => x.Balance).SumAsync();

        return new TotalBalanceResponse
        {
            TotalBalance = total
        };
    }
}
