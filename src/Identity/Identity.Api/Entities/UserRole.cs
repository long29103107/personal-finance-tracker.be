using Microsoft.AspNetCore.Identity;
using Shared.Domain.Abstractions;

namespace Identity.Api.Entities;

public class UserRole : IdentityUserRole<int>, IDateTimeTracking, IAuthorTracking
{
    public string CreatedBy { get; set; } = string.Empty!;
    public string UpdatedBy { get; set; } = string.Empty!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
}

