using Identity.Api.Attributes;
using Identity.Api.Contants;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[LonGAuth(ScopeCodeContants.Post, OperationCodeContants.Read)]
public class RolesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetRoles()
    {
        List<string> roles = ["Admin", "Member"];

        return Ok(roles);
    }
}
