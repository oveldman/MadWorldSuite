using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using JetBrains.Annotations;
using MadWorld.Backend.Application.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Newtonsoft.Json;

namespace MadWorld.Backend.API.Shared.Authorization;

[UsedImplicitly]
public class AuthorizeMiddleWare : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
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

        var request = await context.GetHttpRequestDataAsync();
        if (context.IsEndpointAnonymous() || (request?.Url.IsLocalHost() ?? false))
        {
            await next(context);
            return;
        }

        await SetUnauthorized(context, request);
    }

    private static string GetBearerToken(FunctionContext context)
    {
        var jsonHeaders = context.BindingContext.BindingData["Headers"] as string ?? string.Empty;
        var headers = JsonConvert.DeserializeObject<HttpHeaders>(jsonHeaders) ?? new HttpHeaders();
        return headers.Authorization.Replace("Bearer ", string.Empty);
    }
    
    private static ClaimsPrincipal GetPrincipalFromToken(FunctionContext context, string bearerToken)
    {
        var tokenDecoder = new JwtSecurityTokenHandler();
        var jwtSecurityToken = (JwtSecurityToken)tokenDecoder.ReadToken(bearerToken);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(jwtSecurityToken.Claims, "Bearer", "name", "role"));

        context.Features.Set(principal);
        return principal;
    }

    private static async Task SetUnauthorized(FunctionContext context, HttpRequestData? request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        var res = request.CreateResponse();
        res.StatusCode = HttpStatusCode.Unauthorized;
        await res.WriteStringAsync("401 - Unauthorized!!!");
        context.GetInvocationResult().Value = res;
    }
}