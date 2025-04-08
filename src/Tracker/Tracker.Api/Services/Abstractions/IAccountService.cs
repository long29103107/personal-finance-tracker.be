using Tracker.Api.Entities;

namespace Tracker.Api.Services.Abstractions;

public interface IAccountService
{
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task<Account> CreateAsync(Account account);
    Task<Account?> UpdateAsync(int id, Account updated);
    Task<bool> DeleteAsync(int id);
}

