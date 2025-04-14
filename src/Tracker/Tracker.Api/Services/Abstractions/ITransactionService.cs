using static Shared.Dtos.Tracker.TransactionDtos;

namespace Tracker.Api.Services.Abstractions;

public interface ITransactionService
{
    Task<IEnumerable<TransactionResponse>> GetListAsync();
    Task<TransactionResponse> GetByIdAsync(int id);
    Task<TransactionResponse> CreateAsync(TransactionCreateRequest request);
    Task<TransactionResponse> UpdateAsync(int id, TransactionUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<TransactionDashboardResponse> GetDashboardAsync(DateTime? fromDate = null, DateTime? toDate = null);
}
