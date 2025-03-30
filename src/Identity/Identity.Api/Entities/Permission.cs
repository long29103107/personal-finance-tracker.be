
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Entities;

public class Permission //: AuditEntity<int>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int OperationId { get; set; }
    public Operation Operation { get; set; }

    public int ScopeId { get; set; }
    public Scope Scope { get; set; }

    public virtual ICollection<AccessRule> AccessRules { get; set; } = new List<AccessRule>();
}

