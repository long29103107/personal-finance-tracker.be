using Newtonsoft.Json;

namespace Shared.Dtos.Abstractions;
public interface IResponse
{
    [JsonIgnore]
    int StatusCode { get; set; }
}