using Shared.Domain.Abstractions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Api.Entities;

public class Operation : BaseEntity
{
    public string Code { get; set; }
    public string Name { get; set; }
}

    