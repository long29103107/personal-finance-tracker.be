using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Domain;
public abstract class Request : IRequest
{
    [JsonIgnore]
    [BindNever]
    public ScopedContext? ScopedContext { get; set; }
}