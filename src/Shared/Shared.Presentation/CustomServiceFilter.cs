using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Domain;

namespace Shared.Presentation;
public class CustomServiceFilter : IActionFilter
{
    protected IScopedCache _scopedCache;

    public CustomServiceFilter(IScopedCache scopedCache)
    {
        _scopedCache = scopedCache;
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