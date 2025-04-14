using Shared.Service.Abstractions;
using static Shared.Dtos.Identity.AuthDtos;
using static Shared.Dtos.Identity.TokenDtos;

namespace Identity.Api.Services.Abstractions;

public interface IAuthService : IBaseService
{
    Task<TokenResponse> LoginGoogleAsync(GoogleLoginRequest request);
}
