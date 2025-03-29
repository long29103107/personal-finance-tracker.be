using Identity.Api.Dtos.Auth;
using Shared.Service.Abstractions;

namespace Identity.Api.Services.Abstractions;

public interface IAuthService// : IBaseService<IdentityDbContext>
{
    Task<TokenResponse> LoginGoogleAsync(GoogleLoginRequest request);
}
