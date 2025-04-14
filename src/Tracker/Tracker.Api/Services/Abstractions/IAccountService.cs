using static Shared.Dtos.Tracker.AccountDtos;

namespace Tracker.Api.Services.Abstractions;

public interface IAccountService
{
    Task<IEnumerable<AccountListResponse>> GetListAsync();
    Task<AccountResponse> GetByIdAsync(int id);
    Task<AccountResponse> CreateAsync(AccountCreateRequest request);
    Task<AccountResponse> UpdateAsync(int id, AccountUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<AccountSummaryResponse> GetAccountBalancesAsync(AccountSummaryRequest request);
}

