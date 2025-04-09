using Shared.Domain;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Budget;

public sealed class BudgetUpdateRequest : Request
{
    public int CategoryId { get; set; }
    public decimal Limit { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
