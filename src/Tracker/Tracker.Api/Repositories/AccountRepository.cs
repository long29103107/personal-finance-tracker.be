using Microsoft.EntityFrameworkCore;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;

namespace Tracker.Api.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly FinancialDbContext _context;
    public AccountRepository(FinancialDbContext context) => _context = context;

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Accounts.ToListAsync();
    }

    public async Task<Account?> GetByIdAsync(int id)
    {
        return await _context.Accounts.FindAsync(id);
    }

    public async Task<Account> CreateAsync(Account account)
    {
        _context.Accounts.Add(account);
        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<Account?> UpdateAsync(int id, Account updated)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account is null) return null;

        account.Name = updated.Name;
        account.Balance = updated.Balance;
        account.Currency = updated.Currency;

        await _context.SaveChangesAsync();
        return account;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var account = await _context.Accounts.FindAsync(id);
        if (account is null) return false;

        _context.Accounts.Remove(account);
        await _context.SaveChangesAsync();
        return true;
    }
}


