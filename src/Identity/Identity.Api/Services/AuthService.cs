using Identity.Api.Services.Abstractions;
using Shared.Service;
using Identity.Api.Dtos.Auth;

namespace Identity.Api.Services;

public class AuthService : IAuthService//: BaseService<IdentityDbContext>, IAuthService
{
    //public AuthService(IdentityDbContext context) : base(context)
    //{
    //}

    public async Task<TokenResponse> LoginGoogleAsync(GoogleLoginRequest request)
    {
        var httpClient = new HttpClient();
        var googleApiUrl = $"https://oauth2.googleapis.com/tokeninfo?id_token={request.Token}";

        var response = await httpClient.GetAsync(googleApiUrl);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Invalid Google token");
        }

        //var payload = await response.Content.ReadFromJsonAsync<GoogleUserPayloadResponse>();

        // Xử lý đăng nhập hoặc tạo user nếu chưa có trong database
        //var user = await _userService.FindOrCreateUserAsync(payload);

        // Tạo JWT token để trả về client
        //var jwtToken = _tokenService.GenerateToken(user);

        return new TokenResponse
        {

        };
    }
}
