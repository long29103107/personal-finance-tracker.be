using Identity.Api.Entities;
using Identity.Api.Repositories.Abstractions;
using Shared.Service.Abstractions;
using static Shared.Dtos.Identity.PermissionDtos;
using static Shared.Dtos.Identity.RoleDtos;
namespace Identity.Api.Services.Abstractions;

public interface IRoleService : IBaseService<IRepositoryManager>
{
    IQueryable<Role> _RoleIgnoreGlobalFilter();
    Task<IEnumerable<RoleResponse>> GetListAsync(RoleListRequest request);
    Task<RoleResponse> CreateAsync(RoleCreateRequest request);
    Task<RoleResponse> GetAsync(int id);
    Task<RoleResponse> GetActiveAsync(int id);
    Task<RoleResponse> UpdateAsync(int id, RoleUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<PermissionResponse>> GetPermissionsByRoleAsync(int roleId);
    Task<PermissionResponse> GetPermissionByRoleAsync(int roleId, int permissionId);
}
