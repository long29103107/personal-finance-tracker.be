

namespace Shared.Domain.Abstractions;

public abstract class AuditTracking : IAuditTracking
{
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
