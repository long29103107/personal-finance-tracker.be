using Shared.Domain;
using System.ComponentModel.DataAnnotations;
using Tracker.Api.Constants;

namespace Tracker.Api.Dtos.Budget;

public sealed class BudgetCreateRequest : Request
{
    public int CategoryId { get; set; }
    public decimal Limit { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
