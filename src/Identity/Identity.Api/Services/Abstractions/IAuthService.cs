using Identity.Api.Repositories.Abstractions;
using Shared.Service.Abstractions;
using static Shared.Dtos.Identity.AuthDtos;
using static Shared.Dtos.Identity.TokenDtos;

namespace Identity.Api.Services.Abstractions;

public interface IAuthService : IBaseService<IRepositoryManager>
{
    Task<TokenResponse> LoginGoogleAsync(GoogleLoginRequest request);
}
