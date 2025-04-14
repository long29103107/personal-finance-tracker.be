using Identity.Api.Repositories.Abstractions;
using Shared.Service.Abstractions;
using static Shared.Dtos.Identity.PermissionDtos;

namespace Identity.Api.Services.Abstractions;

public interface IPermissionService : IBaseService<IRepositoryManager>
{
    Task<PermissionResponse> GetAsync(int id);
    Task<IEnumerable<PermissionResponse>> GetListAsync(PermissionListRequest request);
    Task<List<PermissionGrpListByRoleResponse>> GetPermissionByRoleIdAsync(PermissionListByRoleRequest request);
    Task<bool> HasPermissionAsync(int userId, string permission);
    Task<bool> HasPermissionAsync(int userId, int permissionId);
}
