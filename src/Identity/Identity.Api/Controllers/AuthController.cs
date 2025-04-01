using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Identity.Api.Dtos.Auth;
using Identity.Api.Services.Abstractions;
using Identity.Api.Attributes;
using System.Security.Claims;

namespace Identity.Api.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("google")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action("GoogleResponse", "Auth");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpPost("google-login")]
    public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
    {
        return Ok(await _authService.LoginGoogleAsync(request));
    }

    [HttpPost("logout")]
    [LonGAuth]
    public async Task<IActionResult> Logout()
    {
        // Đăng xuất nếu dùng ASP.NET Core Identity
        await HttpContext.SignOutAsync();
        HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());

        return Ok(new { message = "Logout successful" });
    }
}