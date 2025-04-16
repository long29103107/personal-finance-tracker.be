using Identity.Api.Entities;
using System.Collections;
using System.Collections.Generic;
using static Shared.Dtos.Identity.SeedDtos;
using static Shared.Dtos.Identity.UserDtos;

namespace Identity.Api.DependencyInjection.Extensions.Mappings;

public static class RoleMappingExtensions
{
    public static Role ToRole(this RoleRequest request)
    {
        return new Role()
        {
            Code = request.Code,
            Name = request.Name,
            Weight = request.Weight,
            IsLocked = request.IsLocked,
            NormalizedName = request.NormalizedName,
            CreatedBy = "seed",
            CreatedAt = DateTime.UtcNow,
        };
    }

    public static List<Role> ToRoleList(this List<RoleRequest> RoleRequests)
    {
        return RoleRequests.Select(x => x.ToRole()).ToList();
    }
}
