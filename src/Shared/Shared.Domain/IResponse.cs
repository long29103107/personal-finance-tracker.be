using Newtonsoft.Json;

namespace Shared.Domain;
public interface IResponse
{
    [JsonIgnore]
    int StatusCode { get; set; }
}