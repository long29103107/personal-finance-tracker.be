using Identity.Api.Entities;
using static Shared.Dtos.Identity.SeedDtos;

namespace Identity.Api.DependencyInjection.Extensions.Mappings;

public static class ScopeMappingExtensions
{
    public static Scope ToScope(this ScopeRequest scopeRequest)
    {
        return new Scope()
        {
            Code = scopeRequest.Code,
            Name = scopeRequest.Name,
            CreatedBy = "seed",
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static List<Scope> ToScopeList(this List<ScopeRequest> scopeRequest)
    {
        return scopeRequest.Select(x => x.ToScope()).ToList();
    }
}
