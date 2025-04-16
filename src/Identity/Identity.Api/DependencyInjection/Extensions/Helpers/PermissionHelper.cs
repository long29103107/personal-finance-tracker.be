using Identity.Api.Entities;

namespace Identity.Api.DependencyInjection.Extensions.Helpers;

public static class PermissionHelper
{
    public static string GetPermissionCode(this Permission permission)
    {
        return $"{permission.Scope.Code}.{permission.Operation.Code}";
    }

    public static string GetPermissionName(this Permission permission)
    {
        return $"{permission.Scope.Name} {permission.Operation.Name}";
    }
}
