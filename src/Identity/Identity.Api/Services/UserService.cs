using Identity.Api.Dtos.User;
using Identity.Api.Entities;
using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;

    public UserService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<User> CreateOrFindUserAsync(CreateOrFindRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            return user;
        }

        user = new User
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception($"Failed to create user: {string.Join(", ", result.Errors)}");
        }

        return user;
    }
}
