using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class RolesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRoles()
    {
        List<string> roles = ["Admin", "Member"];

        return Ok(roles);
    }
}
