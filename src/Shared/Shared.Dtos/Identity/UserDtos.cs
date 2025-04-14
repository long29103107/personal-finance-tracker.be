using Shared.Dtos.Abstractions;
using Shared.Dtos.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Dtos.Identity;

public static class UserDtos
{
    #region Request
    public sealed class UserListRequest : ListRequest
    {
        public override List<string> PropertiesWhiteList
           => LinqExtensions.GetPropertiesAsString<UserResponse>();

        /// <summary>
        ///     Sort set: All fiels in response
        /// </summary>
        public override string Sort { get; set; }
            = LinqExtensions.GetPropertiesDefaultSortAsString<UserResponse>(
                $"-{nameof(UserResponse.Id).ToLower()}"
            );
    }

    public sealed class UserCreateRequest : Request
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        public bool IsLocked { get; } = false;
        [NotMapped]
        public bool IsActive { get; } = false;
    }

    public sealed class UserUpdatePartialRequest : Request
    {
        [Required]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public sealed class UserUpdateRequest : Request
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [NotMapped]
        public bool IsLocked { get; } = false;
        [NotMapped]
        public bool IsActive { get; } = false;
    }

    public sealed class UserDeleteRequest : Request
    {
        public int Id { get; set; }
    }

    public sealed class UserHasPermissionRequest : Request
    {
        public string ScopeCode { get; set; }
        public string OperationCode { get; set; }
    }

    public sealed class UserCreateOrFindRequest : Request
    {
        public string Email { get; set; }
    }

    #endregion

    #region Response
    public sealed class UserListResponse : Response
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public sealed class UserResponse : Response
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }
        public string CreatedBy { get; set; } = string.Empty!;
        public string UpdatedBy { get; set; } = string.Empty!;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class UserCreateOrFindResponse : Response
    {
        public string Email { get; set; }
        public string Username { get; set; }
    }

    #endregion
}