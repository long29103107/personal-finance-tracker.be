using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using static Shared.Dtos.Identity.RoleDtos;

namespace MyBlog.Identity.Api.Controllers;

public class RolesController : CustomControllerBase
{
    private readonly IRoleService _service;

    public RolesController(IRoleService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] RoleListRequest request)
    {
        return GetResponse(await _service.GetListAsync(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return GetResponse(await _service.GetAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] RoleCreateRequest request)
    {
        return GetResponse(await _service.CreateAsync(request));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] RoleUpdateRequest request)
    {
        return GetResponse(await _service.UpdateAsync(id, request));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdatePartialAsync([FromRoute] int id, [FromBody] JsonPathRequest<RoleUpdatePartialRequest> request)
    {
        return GetResponse(await _service.UpdatePartialAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        return GetResponse(await _service.DeleteAsync(id));
    }

    [HttpGet("{id}/permissions")]
    public async Task<IActionResult> GetPermissionsByRoleAsync([FromRoute] int id)
    {
        return GetResponse(await _service.GetPermissionsByRoleAsync(id));
    }


    [HttpGet("{roleId}/permissions/{permissionId}")]
    public async Task<IActionResult> GetPermissionByRoleAsync([FromRoute] int roleId, [FromRoute] int permissionId)
    {
        return GetResponse(await _service.GetPermissionByRoleAsync(roleId, permissionId));
    }
}
