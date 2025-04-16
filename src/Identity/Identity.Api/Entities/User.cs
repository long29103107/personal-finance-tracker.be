using Microsoft.AspNetCore.Identity;
using Shared.Domain.Abstractions;

namespace Identity.Api.Entities;

public class User : IdentityUser<int>, IDateTimeTracking, IAuthorTracking
{
    public string CreatedBy { get; set; } = string.Empty!;
    public string UpdatedBy { get; set; } = string.Empty!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsLocked { get; set; }
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
