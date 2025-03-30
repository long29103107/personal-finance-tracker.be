using Identity.Api.Entities;

namespace Identity.Api.Services.Abstractions;

public interface ITokenService
{
    Task<string> GenerateJwtTokenAsync(User user);
}
