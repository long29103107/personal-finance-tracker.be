using Identity.Api.Dtos.Auth;
using Identity.Api.Services.Abstractions;

namespace Identity.Api.Services;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public AuthService(IUserService userService, ITokenService tokenService)
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
