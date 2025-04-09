using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using ValidationException = Shared.Domain.Exceptions.ValidationException;
using Tracker.Api.Dtos.Transaction;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;
using Tracker.Api.Exceptions;
using Tracker.Api.DependencyInjection.Extensions.Mapping;
using Tracker.Api.Constants;

namespace Tracker.Api.Services;

public class TransactionService(ITransactionRepository _transactionRepo
    , IValidatorFactory _validatorFactory) : ITransactionService
{
    public async Task<IEnumerable<TransactionResponse>> GetListAsync()
    {
        var result = await _transactionRepo.FindAll()
            .Select(x => new TransactionResponse()
            {
                Id = x.Id,
                AccountId = x.AccountId,
                CategoryId = x.CategoryId,
                Amount = x.Amount,
                Type = x.Type,
                Date = x.Date,
                Description = x.Description
            })
            .ToListAsync();

        return result;
    }

    public async Task<TransactionResponse> GetByIdAsync(int id)
    {
        Transaction result = await _GetTransactionAsync(id)
           ?? throw new TransactionException.NotFound(id);

        return result.ToTransactionResponse();
    }

    public async Task<TransactionResponse> CreateAsync(TransactionCreateRequest request)
    {
        var entity = new Transaction
        {
            AccountId = request.AccountId,
            CategoryId = request.CategoryId,
            Amount = request.Amount,
            Type = request.Type,
            Date = request.Date,
            Description = request.Description
        };

        await _ValidateTransactionAsync(entity);
        _transactionRepo.Add(entity);
        await _transactionRepo.SaveAsync();

        return entity.ToTransactionResponse();
    }

    public async Task<TransactionResponse> UpdateAsync(int id, TransactionUpdateRequest request)
    {
        Transaction entity = await _GetTransactionAsync(id)
          ?? throw new TransactionException.NotFound(id);

        entity.AccountId = request.AccountId;
        entity.CategoryId = request.CategoryId;
        entity.Amount = request.Amount;
        entity.Type = request.Type;
        entity.Date = request.Date;
        entity.Description = request.Description;

        await _ValidateTransactionAsync(entity);
        _transactionRepo.Update(entity);
        await _transactionRepo.SaveAsync();

        return entity.ToTransactionResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Transaction entity = await _GetTransactionAsync(id)
          ?? throw new TransactionException.NotFound(id);

        try
        {
            _transactionRepo.Remove(entity);
            await _transactionRepo.SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<Transaction?> _GetTransactionAsync(int id)
    {
        var result = await _transactionRepo.FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync();

        return result;
    }

    private async Task _ValidateTransactionAsync(Transaction model)
    {
        var validator = _validatorFactory.GetValidator<Transaction>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new ValidationException(errors);
        }
    }

}
