using Tracker.Api.Dtos.Goal;

namespace Tracker.Api.Services.Abstractions;

public interface IGoalService
{
    Task<IEnumerable<GoalResponse>> GetListAsync();
    Task<GoalResponse> GetByIdAsync(int id);
    Task<GoalResponse> CreateAsync(GoalCreateRequest request);
    Task<GoalResponse> UpdateAsync(int id, GoalUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
