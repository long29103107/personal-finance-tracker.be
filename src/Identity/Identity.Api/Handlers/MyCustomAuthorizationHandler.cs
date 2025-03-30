using Identity.Api.Attributes;
using Identity.Api.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Api.Handlers;

public class MyCustomAuthorizationHandler(ICustomAuthService customAuthService) : AuthorizationHandler<ClaimRequirementAttribute>
{

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ClaimRequirementAttribute requirement)
    {
        if (await customAuthService.CheckIfAllowedAsync(requirement.UserId, requirement.Scope, requirement.Operation))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }

}