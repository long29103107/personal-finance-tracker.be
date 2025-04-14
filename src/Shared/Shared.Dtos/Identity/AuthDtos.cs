using Shared.Dtos.Abstractions;

namespace Shared.Dtos.Identity;

public static class AuthDtos
{
    //Request
    public class GoogleLoginRequest : Request
    {
        public string Token { get; set; }
    }

    //Resposnse
    public class GoogleUserPayloadResponse : Response
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
    }
}
