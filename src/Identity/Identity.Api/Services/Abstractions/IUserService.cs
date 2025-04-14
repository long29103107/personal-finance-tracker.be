using Identity.Api.Entities;
using Shared.Service.Abstractions;
using static Shared.Dtos.Identity.UserDtos;

namespace Identity.Api.Services.Abstractions;

public interface IUserService : IBaseService
{
    public Task<User> CreateOrFindUserAsync(UserCreateOrFindRequest request);
    Task<UserResponse> GetAsync(int id);
    Task<UserResponse> GetActiveAsync(int id);
    Task<IEnumerable<UserResponse>> GetListAsync(UserListRequest request);
    Task<UserResponse> UpdateAsync(int id, UserUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task AssignedRoleAsync(int userId, int roleId);
    Task<bool> HasPermissionAsync(int userId, int permissionId);
    Task<bool> HasPermissionAsync(int userId, UserHasPermissionRequest request);
}
