using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Shared.Domain;
public abstract class Response : IResponse
{
    [JsonIgnore]
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
}