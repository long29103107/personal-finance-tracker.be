﻿namespace Shared.Domain;

public class ScopedContext
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string AccessToken { get; set; }
    //public string RemoteIp { get; set; }
    //public NameValueCollection RequestQueryString { get; set; }
    //public string RequestPath { get; set; }
}
