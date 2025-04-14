using static Shared.Dtos.Tracker.GoalDtos;

namespace Tracker.Api.Services.Abstractions;

public interface IGoalService
{
    Task<IEnumerable<GoalResponse>> GetListAsync();
    Task<GoalResponse> GetByIdAsync(int id);
    Task<GoalResponse> CreateAsync(GoalCreateRequest request);
    Task<GoalResponse> UpdateAsync(int id, GoalUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<bool> MarkAsCompletedAsync(int id);
    Task UpdateProgressAsync(int goalId, decimal transactionAmount);
}
