using Tracker.Api.Entities;

namespace Tracker.Api.Repositories.Abstractions;

public interface IAccountRepository
{
    Task<IEnumerable<Account>> GetAllAsync();
    Task<Account?> GetByIdAsync(int id);
    Task<Account> CreateAsync(Account account);
    Task<Account?> UpdateAsync(int id, Account updated);
    Task<bool> DeleteAsync(int id);
}