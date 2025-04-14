using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Shared.Domain.Abstractions;

namespace Identity.Api.Entities;

public class Scope : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
}

