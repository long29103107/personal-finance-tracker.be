
using Shared.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Entities;

public class Permission : BaseEntity
{
    public int OperationId { get; set; }
    public Operation Operation { get; set; }

    public int ScopeId { get; set; }
    public Scope Scope { get; set; }

    public virtual ICollection<AccessRule> AccessRules { get; set; } = new List<AccessRule>();
}

