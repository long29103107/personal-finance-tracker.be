using System.Collections.Specialized;

namespace Shared.Domain;

public class ScopedContext
{
    public bool IsTransactionOrdinary { get; set; } = false;
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string RemoteIp { get; set; }
    public string AccessToken { get; set; }
    public NameValueCollection RequestQueryString { get; set; }
    public string RequestPath { get; set; }
}
