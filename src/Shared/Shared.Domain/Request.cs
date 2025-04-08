using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Shared.Domain;
public abstract class Request : IRequest
{
    [JsonIgnore]
    [BindNever]
    //[SwaggerIgnore]
    public ScopedContext? ScopedContext { get; set; }
}