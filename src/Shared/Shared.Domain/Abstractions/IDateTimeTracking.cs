namespace Shared.Domain.Abstractions;

public interface IDateTimeTracking
{
    DateTime CreatedAt { get; set; }
    DateTime? UpdatedAt { get; set; }
}
