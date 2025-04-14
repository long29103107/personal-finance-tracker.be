namespace Shared.Domain.Abstractions;

public interface IAuthorTracking
{
    string CreatedBy { get; set; }
    string? UpdatedBy { get; set; }
}
