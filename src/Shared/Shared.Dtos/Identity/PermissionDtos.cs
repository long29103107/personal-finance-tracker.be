using Shared.Dtos.Abstractions;
using Shared.Dtos.Extensions;

namespace Shared.Dtos.Identity;

public static class PermissionDtos
{
    #region Request
    public sealed class PermissionListRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<PermissionResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<PermissionResponse>(
                $"-{nameof(PermissionResponse.Id).ToLower()}"
            );
    }

    public sealed class PermissionListByRoleRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<PermissionListByRoleResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<PermissionListByRoleResponse>(
                $"-{nameof(PermissionListByRoleResponse.Id).ToLower()}"
            );
    }
    #endregion

    #region Response

    public abstract class AuditResponse : Response
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public sealed class PermissionListResponse : AuditResponse
    {
        public string Code { get; set; }
        public string Description { get; set; }

    }

    public sealed class PermissionResponse : AuditResponse
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public sealed class PermissionListByRoleResponse : AuditResponse
    {
        public int ScopeId { get; set; }
        public bool IsEnabled { get; set; } = false;
        public string ScopeName { get; set; }
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public string PermissionName { get; set; }
        public int RoleId { get; set; }
    }

    public sealed class PermissionGrpListByRoleResponse : AuditResponse
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; } = false;
        public string Name { get; set; }
        public string PermissionName { get; set; }

        public List<OperationByRoleResponse> Operations = new();
    }

    public sealed class OperationByRoleResponse
    {
        public int Id { get; set; }
        public bool IsEnabled { get; set; } = false;
        public string Name { get; set; }
    }
    #endregion
}