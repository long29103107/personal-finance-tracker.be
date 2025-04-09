using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using Tracker.Api.Dtos.Account;
using Tracker.Api.Entities;
using Tracker.Api.Exceptions;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;
using ValidationException = Shared.Domain.Exceptions.ValidationException;

namespace Tracker.Api.Services;
public class AccountService(IAccountRepository _accountRepo, IValidatorFactory _validatorFactory) : IAccountService
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

        await _ValidateAccountAsync(entity);
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

        entity.Balance = request.Balance;
        entity.Name = request.Name;
        entity.Email = request.Email;
        entity.Currency = request.Currency;

        await _ValidateAccountAsync(entity);
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

    public async Task<AccountSummaryResponse> GetAccountBalancesAsync(AccountSummaryRequest request)
    {
        var email = request.ScopedContext.Email
            ?? throw new ArgumentNullException(nameof(request.ScopedContext.Email));

        var accounts = await _accountRepo.FindByCondition(x => x.Email == email)
            .ToListAsync(); 

        var result =  new AccountSummaryResponse
        {
            Accounts = accounts.Select(a => new AccountSummaryDto
            {
                AccountName = a.Name,
                Balance = a.Balance
            }).ToList()
        };

        return result;
    }

    private async Task<Account?> _GetAccountById(int id)
    {
        var result = await _accountRepo.FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync();

        return result;
    }

    private async Task _ValidateAccountAsync(Account model)
    {
        var validator = _validatorFactory.GetValidator<Account>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new ValidationException(errors);
        }
    }
}