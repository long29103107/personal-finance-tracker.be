using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Entities;

public class AccessRule
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("Permission")]
    public int? PermissionId { get; set; }
    public Permission Permission { get; set; }

    [ForeignKey("Role")]
    public int? RoleId { get; set; }
    public Role Role { get; set; }
    public bool Mode { get; set; } = false;
}
