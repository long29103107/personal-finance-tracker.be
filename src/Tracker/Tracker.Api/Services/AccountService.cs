using Microsoft.EntityFrameworkCore;
using Tracker.Api.Dtos.Account;
using Tracker.Api.Entities;
using Tracker.Api.Exceptions;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;

namespace Tracker.Api.Services;
public class AccountService(IAccountRepository _accountRepo) : IAccountService
{
    public async Task<IEnumerable<AccountListResponse>> GetListAsync()
    {
        var result = await _accountRepo.FindAll()
            .Select(x => new AccountListResponse()
            {
                Email = x.Email,
                Name = x.Name,
                Balance = x.Balance,
                Currency = x.Currency
            })
            .ToListAsync();

        return result;
    }

    public async Task<AccountResponse> GetByIdAsync(int id)
    {
        var entity = await _GetAccountById(id)
            ?? throw new AccountException.NotFound(id);

        return new AccountResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Balance = entity.Balance,
            Currency = entity.Currency
        };
    }

    public async Task<AccountResponse> CreateAsync(AccountCreateRequest request)
    {
        var entity = new Account
        {
            Name = request.Name,
            Email = request.Email,
            Balance = request.Balance,
            Currency = request.Currency
        };

        _ValidateAccount(entity);
        _accountRepo.Add(entity);
        await _accountRepo.SaveAsync();

        return new AccountResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Balance = entity.Balance,
            Currency = entity.Currency
        };
    }

    public async Task<AccountResponse> UpdateAsync(int id, AccountUpdateRequest request)
    {
        var entity = await _GetAccountById(id)
            ?? throw new AccountException.NotFound(id); 

        _ValidateAccount(entity);
        _accountRepo.Update(entity);
        await _accountRepo.SaveAsync();

        return new AccountResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Email = entity.Email,
            Balance = entity.Balance,
            Currency = entity.Currency
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _GetAccountById(id)
           ?? throw new AccountException.NotFound(id);

        try
        {
            _accountRepo.Remove(entity);
            await _accountRepo.SaveAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    private async Task<Account> _GetAccountById(int id)
    {
        var result = await _accountRepo.FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync();

        return result;
    }

    private void _ValidateAccount(Account entity)
    {
        //TODO: Validate account
    }
}