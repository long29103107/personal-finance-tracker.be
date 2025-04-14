using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Shared.Presentation;
using static Shared.Dtos.Identity.UserDtos;

namespace MyBlog.Identity.Api.Controllers;

public class UserController : CustomControllerBase
{
    private readonly IUserService _service;

    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetListAsync([FromQuery] UserListRequest request)
    {
        return GetResponse(await _service.GetListAsync(request));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync([FromRoute] int id)
    {
        return GetResponse(await _service.GetAsync(id));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UserUpdateRequest request)
    {
        return GetResponse(await _service.UpdateAsync(id, request));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        return GetResponse(await _service.DeleteAsync(id));
    }


    [HttpGet("{userId}/permissions/{permissionId}")]
    public async Task<IActionResult> GetPermissionByRoleAsync([FromRoute] int userId, [FromRoute] int permissionId)
    {
        return GetResponse(await _service.HasPermissionAsync(userId, permissionId));
    }

    [HttpGet("{userId}/has-permission")]
    public async Task<IActionResult> HasPermissionAsync([FromRoute] int userId, [FromQuery] UserHasPermissionRequest request)
    {
        return GetResponse(await _service.HasPermissionAsync(userId, request));
    }

    [HttpPost("{userId}/assigned/{roleId}")]
    public async Task<IActionResult> HasPermissionAsync([FromRoute] int userId, [FromRoute] int roleId)
    {
        await _service.AssignedRoleAsync(userId, roleId);
        return GetResponse();
    }
}
