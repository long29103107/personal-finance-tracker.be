using Identity.Api.Repositories.Abstractions;
using Identity.Api.Services.Abstractions;
using Shared.Service;
using static Shared.Dtos.Identity.AuthDtos;
using static Shared.Dtos.Identity.TokenDtos;
using ILogger= Serilog.ILogger;

namespace Identity.Api.Services;

public class AuthService : BaseService<IRepositoryManager>, IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthService(IUserService userService, ITokenService tokenService, ILogger logger, IRepositoryManager repoManager) 
        : base(logger, repoManager)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    public async Task<TokenResponse> LoginGoogleAsync(GoogleLoginRequest request)
    {
        var httpClient = new HttpClient();
        var googleApiUrl = $"https://oauth2.googleapis.com/tokeninfo?id_token={request.Token}";

        var response = await httpClient.GetAsync(googleApiUrl);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Invalid Google token");
        }

        var payload = await response.Content.ReadFromJsonAsync<GoogleUserPayloadResponse>();

        var user = await _userService.CreateOrFindUserAsync(new() { Email = payload.Email } );

        var token = await  _tokenService.GenerateJwtTokenAsync(user);

        return new TokenResponse
        {
            AccessToken = token
        };
    }
}
