using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.Filters;

public sealed class ApiKeyAuthenticationFilter : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new ObjectResult("Authorization header is needed") { StatusCode = StatusCodes.Status401Unauthorized };
            return;
        }

        token = ExtractToken(token);
        var sessionService = GetAuthenticationLogic(context);
        var correctUser = sessionService.IsTokenValid(token);
        if (!correctUser)
        {
            context.Result = new ObjectResult("The token does not correspond to a existing user") { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }

    private static Cine.BusinessLogic.Abstractions.ISessionService GetAuthenticationLogic(AuthorizationFilterContext context)
    {
        return context.HttpContext.RequestServices.GetRequiredService<Cine.BusinessLogic.Abstractions.ISessionService>();
    }

    private static string ExtractToken(string authorizationHeader)
    {
        const string bearerPrefix = "Bearer ";
        if (authorizationHeader.StartsWith(bearerPrefix, StringComparison.OrdinalIgnoreCase))
        {
            return authorizationHeader[bearerPrefix.Length..].Trim();
        }

        return authorizationHeader.Trim();
    }
}
