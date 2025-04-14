using static Shared.Dtos.Tracker.BudgetDtos;

namespace Tracker.Api.Services.Abstractions;

public interface IBudgetService
{
    Task<IEnumerable<BudgetResponse>> GetListAsync();
    Task<BudgetResponse> GetByIdAsync(int id);
    Task<BudgetResponse> CreateAsync(BudgetCreateRequest request);
    Task<BudgetResponse> UpdateAsync(int id, BudgetUpdateRequest request);
    Task<bool> DeleteAsync(int id);
}
