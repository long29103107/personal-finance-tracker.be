using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Dtos;
using Shared.Dtos.Abstractions;

namespace Shared.Presentation;
public class CustomServiceFilter : IActionFilter
{
    protected IScopedCache _scopedCache;

    public CustomServiceFilter(IScopedCache scopedCache)
    {
        _scopedCache = scopedCache;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if(context.ActionArguments.ContainsKey("scopedCache"))
        {
            context.ActionArguments["scopedCache"] = _scopedCache;
        }
        else
        {
            if(_scopedCache.ScopedContext == null)
            {
                _scopedCache.ScopedContext = new ScopedContext();
                _scopedCache.ScopedContext.UserId = 1;
            }
        }
    }
}