using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using JetBrains.Annotations;
using MadWorld.Backend.API.Shared.Functions.Expansions;
using MadWorld.Backend.Application.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Newtonsoft.Json;

namespace MadWorld.Backend.API.Shared.Authorization;

[UsedImplicitly]
public sealed class AuthorizeMiddleWare : IFunctionsWorkerMiddleware
{
    private readonly IFunctionContextWrapper _functionContextWrapper;

    public AuthorizeMiddleWare(IFunctionContextWrapper functionContextWrapper)
    {
        _functionContextWrapper = functionContextWrapper;
    }
    
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (!context.IsHttpTrigger())
        {
            await next(context);
            return;
        }
        
        var bearerToken = GetBearerToken(context);
        
        if (!string.IsNullOrEmpty(bearerToken))
        {
            var claimsPrincipal = GetPrincipalFromToken(context, bearerToken);
            
            var requiredRole = context.GetRequiredRole();
            var user = claimsPrincipal.GetUser();

            if (user.IsInRole(requiredRole))
            {
                await next(context);
                return;   
            }
        }

        var request = await _functionContextWrapper.GetHttpRequestDataAsync(context);
        if (context.IsEndpointAnonymous() || request!.Url.IsLocalHost())
        {
            await next(context);
            return;
        }

        await SetUnauthorized(context, request);
    }

    private static string GetBearerToken(FunctionContext context)
    {
        var jsonHeaders = context.BindingContext.BindingData["Headers"] as string;
        var headers = JsonConvert.DeserializeObject<HttpHeaders>(jsonHeaders!);
        return headers!.Authorization.Replace("Bearer ", string.Empty);
    }
    
    private static ClaimsPrincipal GetPrincipalFromToken(FunctionContext context, string bearerToken)
    {
        var tokenDecoder = new JwtSecurityTokenHandler();
        var jwtSecurityToken = (JwtSecurityToken)tokenDecoder.ReadToken(bearerToken);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(jwtSecurityToken.Claims, "Bearer", "name", "role"));

        context.Features.Set(principal);
        return principal;
    }

    private async Task SetUnauthorized(FunctionContext context, HttpRequestData request)
    {
        var res = request.CreateResponse();
        res.StatusCode = HttpStatusCode.Unauthorized;
        await res.WriteStringAsync("401 - Unauthorized!!!");
        _functionContextWrapper.GetInvocationResult(context).Value = res;
    }
}