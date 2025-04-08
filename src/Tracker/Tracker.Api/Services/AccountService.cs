using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repo;
    public AccountService(IAccountRepository repo) => _repo = repo;
    public Task<IEnumerable<Account>> GetAllAsync() => _repo.GetAllAsync();
    public Task<Account?> GetByIdAsync(int id) => _repo.GetByIdAsync(id);
    public Task<Account> CreateAsync(Account account) => _repo.CreateAsync(account);
    public Task<Account?> UpdateAsync(int id, Account updated) => _repo.UpdateAsync(id, updated);
    public Task<bool> DeleteAsync(int id) => _repo.DeleteAsync(id);
}