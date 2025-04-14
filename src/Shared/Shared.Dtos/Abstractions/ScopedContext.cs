namespace Shared.Dtos.Abstractions;

public class ScopedContext
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    //public string RemoteIp { get; set; }
    //public NameValueCollection RequestQueryString { get; set; }
    //public string RequestPath { get; set; }
}
