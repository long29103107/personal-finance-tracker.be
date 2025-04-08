using Shared.Domain.Abstractions;

namespace Tracker.Api.Entities;

public class Budget : BaseEntity
{
    public string Email { get; set; }
    public int CategoryId { get; set; }
    public decimal Limit { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Category Category { get; set; } = null!;
}
