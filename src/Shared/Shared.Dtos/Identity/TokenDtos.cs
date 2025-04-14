using Shared.Domain;
using Shared.Dtos.Abstractions;
using System.Security.Claims;

namespace Shared.Dtos.Identity;

public static class TokenDtos
{
    public sealed class TokenRequest : Request
    {
        public List<Claim> Claims { get; set; } = new List<Claim>();
    }

    public class TokenResponse
    {
        public string AccessToken { get; set; }
    }

}
