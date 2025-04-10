using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Exceptions;
using ValidationException = Shared.Domain.Exceptions.ValidationException;
using Tracker.Api.Dtos.Goal;
using Tracker.Api.Entities;
using Tracker.Api.Repositories.Abstractions;
using Tracker.Api.Services.Abstractions;
using Tracker.Api.Exceptions;
using Tracker.Api.DependencyInjection.Extensions.Mapping;
using Tracker.Api.Repositories;

namespace Tracker.Api.Services;

public class GoalService(IGoalRepository _goalRepo
    , IValidatorFactory _validatorFactory) : IGoalService
{
    public async Task<IEnumerable<GoalResponse>> GetListAsync()
    {
        var result = await _goalRepo.FindAll()
            .Select(x => new GoalResponse()
            {
                Id = x.Id,
                Name = x.Name,
                TargetAmount = x.TargetAmount,
                CurrentAmount = x.CurrentAmount,
                Deadline = x.Deadline
            })
            .ToListAsync();

        return result;
    }

    public async Task<GoalResponse> GetByIdAsync(int id)
    {
        Goal result = await _GetGoalAsync(id)
           ?? throw new GoalException.NotFound(id);

        return result.ToGoalResponse();
    }

    public async Task<GoalResponse> CreateAsync(GoalCreateRequest request)
    {
        var entity = new Goal
        {
            Name = request.Name,
            TargetAmount = request.TargetAmount,
            CurrentAmount = request.CurrentAmount,
            Deadline = request.Deadline
        };

        await _ValidateGoalAsync(entity);
        _goalRepo.Add(entity);
        await _goalRepo.SaveAsync();

        return entity.ToGoalResponse();
    }

    public async Task<GoalResponse> UpdateAsync(int id, GoalUpdateRequest request)
    {
        Goal entity = await _GetGoalAsync(id)
          ?? throw new GoalException.NotFound(id);

        entity.Name = request.Name;
        entity.TargetAmount = request.TargetAmount;
        entity.CurrentAmount = request.CurrentAmount;
        entity.Deadline = request.Deadline;

        await _ValidateGoalAsync(entity);
        _goalRepo.Update(entity);
        await _goalRepo.SaveAsync();

        return entity.ToGoalResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Goal entity = await _GetGoalAsync(id)
          ?? throw new GoalException.NotFound(id);

        try
        {
            _goalRepo.Remove(entity);
            await _goalRepo.SaveAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> MarkAsCompletedAsync(int id)
    {
        var goal = await _GetGoalAsync(id);
        if (goal == null) return false;

        goal.CurrentAmount = goal.TargetAmount;
        _goalRepo.Update(goal);
        await _goalRepo.SaveAsync();

        return true;
    }

    public async Task UpdateProgressAsync(int goalId, decimal transactionAmount)
    {
        var goal = await _GetGoalAsync(goalId);
        if (goal == null) return;

        goal.CurrentAmount += transactionAmount;

        if (goal.CurrentAmount > goal.TargetAmount)
            goal.CurrentAmount = goal.TargetAmount;

        _goalRepo.Update(goal);
        await _goalRepo.SaveAsync();
    }

    private async Task<Goal?> _GetGoalAsync(int id)
    {
        var result = await _goalRepo.FindByCondition(x => x.Id == id)
           .FirstOrDefaultAsync();

        return result;
    }

    private async Task _ValidateGoalAsync(Goal model)
    {
        var validator = _validatorFactory.GetValidator<Goal>();
        var result = await validator.ValidateAsync(model);

        if (!result.IsValid)
        {
            var errors = result.Errors.Select(x => new ValidationError(x.PropertyName, x.ErrorMessage)).ToList();

            throw new ValidationException(errors);
        }
    }

}
