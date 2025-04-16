using Identity.Api.DependencyInjection.Extensions.Helpers;
using Identity.Api.Entities;
using static Shared.Dtos.Identity.PermissionDtos;
using static Shared.Dtos.Identity.UserDtos;

namespace Identity.Api.DependencyInjection.Extensions.Mappings;

public static class PermissionMappingExtensions
{
    public static PermissionResponse ToPermissionResponse(this Permission permission)
    {
        return new PermissionResponse()
        {
            Id = permission.Id,
            Code = permission.GetPermissionCode(),
            Name = permission.GetPermissionName(),
            CreatedAt = permission.CreatedAt,
            UpdatedAt = permission.UpdatedAt,
            CreatedBy = permission.CreatedBy,
            UpdatedBy = permission.UpdatedBy,
        };
    }
}
