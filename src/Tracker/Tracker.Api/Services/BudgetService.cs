using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using ValidationException = Shared.Domain.Exceptions.ValidationException;
using Tracker.Api.Dtos.Budget;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;
using Tracker.Api.Exceptions;
using Tracker.Api.DependencyInjection.Extensions.Mapping;

namespace Tracker.Api.Services;

public class BudgetService(IBudgetRepository _budgetRepo
    , IValidatorFactory _validatorFactory) : IBudgetService
{
    public async Task<IEnumerable<BudgetResponse>> GetListAsync()
    {
        var result = await _budgetRepo.FindAll()
            .Select(x => new BudgetResponse()
            {
                Id = x.Id,
                CategoryId = x.CategoryId,
                Limit = x.Limit,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            })
            .ToListAsync();

        return result;
    }

    public async Task<BudgetResponse> GetByIdAsync(int id)
    {
        Budget result = await _GetBudgetAsync(id)
           ?? throw new BudgetException.NotFound(id);

        return result.ToBudgetResponse();
    }

    public async Task<BudgetResponse> CreateAsync(BudgetCreateRequest request)
    {
        var entity = new Budget
        {
            CategoryId = request.CategoryId,
            Limit = request.Limit,
            StartDate = request.StartDate,
            EndDate = request.EndDate
        };

        await _ValidateBudgetAsync(entity);
        _budgetRepo.Add(entity);
        await _budgetRepo.SaveAsync();

        return entity.ToBudgetResponse();
    }

    public async Task<BudgetResponse> UpdateAsync(int id, BudgetUpdateRequest request)
    {
        Budget entity = await _GetBudgetAsync(id)
          ?? throw new BudgetException.NotFound(id);

        entity.Limit = request.Limit;
        entity.CategoryId = request.CategoryId;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;

        await _ValidateBudgetAsync(entity);
        _budgetRepo.Update(entity);
        await _budgetRepo.SaveAsync();

        return entity.ToBudgetResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Budget entity = await _GetBudgetAsync(id)
          ?? throw new BudgetException.NotFound(id);

        try
        {
            _budgetRepo.Remove(entity);
            await _budgetRepo.SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<Budget?> _GetBudgetAsync(int id)
    {
        var result = await _budgetRepo.FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync();

        return result;
    }

    private async Task _ValidateBudgetAsync(Budget model)
    {
        var validator = _validatorFactory.GetValidator<Budget>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new ValidationException(errors);
        }
    }

}
