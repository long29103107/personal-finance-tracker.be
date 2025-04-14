using Shared.Dtos.Abstractions;
using Shared.Dtos.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Dtos.Identity;

public static class RoleDtos
{
    #region Request
    public sealed class RoleListRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<RoleResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<RoleResponse>(
                $"-{nameof(RoleResponse.Id).ToLower()}"
            );
    }

    public sealed class RoleCreateRequest : Request
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        [NotMapped]
        public bool IsLocked { get; } = false;
    }

    public sealed class RoleUpdatePartialRequest : Request
    {
        public bool IsActive { get; set; }
    }

    public sealed class RoleUpdateRequest : Request
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public sealed class RoleDeleteRequest : Request
    {
        public int Id { get; set; }
    }
    #endregion

    #region Response
    public sealed class RoleListResponse : Response
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsLocked { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public sealed class RoleResponse : Response
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion
}