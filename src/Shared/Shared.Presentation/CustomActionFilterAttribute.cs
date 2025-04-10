using Microsoft.AspNetCore.Mvc.Filters;

namespace Shared.Presentation;
public class CustomActionFilterAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
    }
}