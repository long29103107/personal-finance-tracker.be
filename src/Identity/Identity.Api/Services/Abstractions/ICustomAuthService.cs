namespace Identity.Api.Services.Abstractions;

public interface ICustomAuthService
{
    Task<bool> CheckIfAllowedAsync(int userId, string scope, string operation);
}
