using Shared.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Entities;

public class AccessRule : BaseEntity
{

    [ForeignKey("Permission")]
    public int? PermissionId { get; set; }
    public Permission Permission { get; set; }

    [ForeignKey("Role")]
    public int? RoleId { get; set; }
    public Role Role { get; set; }
    public bool Mode { get; set; } = false;
}
