﻿using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Identity.Api.Services.Abstractions;

namespace Identity.Api.Attributes;


public class LonGAuth : Attribute, IAsyncAuthorizationFilter
{
    private string _scope = string.Empty;
    private string _operation = string.Empty;

    public LonGAuth()
    {
    }


    public LonGAuth(string scope, string operation)
    {
        _scope = scope;
        _operation = operation;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();

        try
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var claimsPrincipal = _ValidateToken(token, configuration);
            if (claimsPrincipal == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var myCustomAuthService = context.HttpContext.RequestServices.GetRequiredService<ICustomAuthService>();


            var email = claimsPrincipal.FindFirst("Email")?.Value ?? string.Empty;

            //if (!email.Equals("long29103107@gmail.com", StringComparison.OrdinalIgnoreCase))
            //{
            //    context.Result = new ObjectResult("Forbidden") { StatusCode = StatusCodes.Status403Forbidden };
            //    return;
            //}


            //var userId = int.TryParse(claimsPrincipal.FindFirst("Id")?.Value, out var userIdClaim)
            //    ? userIdClaim
            //    : 0;

            //if (!string.IsNullOrEmpty(_scope)
            //    && !string.IsNullOrEmpty(_operation)
            //    && !await myCustomAuthService.CheckIfAllowedAsync(userId, _scope, _operation))
            //{
            //    context.Result = new ObjectResult("Forbidden") { StatusCode = StatusCodes.Status403Forbidden };
            //    return;
            //}

            //TODO: Add Scoped Cache
            //var scopeCache = context.HttpContext.RequestServices.GetRequiredService<IScopedCache>();


            // Set the authenticated user
            context.HttpContext.User = claimsPrincipal;
        }
        catch (Exception e)
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private ClaimsPrincipal _ValidateToken(string token, IConfiguration configuration)
    {
        var handler = new JwtSecurityTokenHandler();
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidAudience = configuration["JwtSettings:ValidAudience"],
            ValidIssuer = configuration["JwtSettings:ValidIssuer"],
            RequireExpirationTime = false,
            ValidateLifetime = false
        };

        SecurityToken validatedToken;
        ClaimsPrincipal principal = handler.ValidateToken(token, validationParameters, out validatedToken);

        validatedToken = handler.ReadJwtToken(token);

        return principal;
    }
}