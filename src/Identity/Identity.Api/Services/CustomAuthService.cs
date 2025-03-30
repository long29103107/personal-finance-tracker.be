using Identity.Api.Services.Abstractions;

namespace Identity.Api.Services;

public class CustomAuthService(IHttpContextAccessor contextAccessor) : ICustomAuthService
{
    public async Task<bool> CheckIfAllowedAsync(int userId, string scope, string operation)
    {
        var result = false;

        var url = $"http://localhost:5001/api/user/{userId}/has-permission?scopeCode={scope}&operationCode={operation}";

        var client = new HttpClient();
        var response = await client.GetAsync(url);
        response.EnsureSuccessStatusCode();

        result = bool.TryParse(await response.Content.ReadAsStringAsync(), out var output) ? output : false;

        return true;
    }
}
