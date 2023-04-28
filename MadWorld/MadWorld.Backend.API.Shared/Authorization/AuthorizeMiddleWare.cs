using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using JetBrains.Annotations;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Newtonsoft.Json;

namespace MadWorld.Backend.API.Shared.Authorization;

[UsedImplicitly]
public class AuthorizeMiddleWare : IFunctionsWorkerMiddleware
{
    public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var jsonHeaders = context.BindingContext.BindingData["Headers"] as string ?? string.Empty;
        var headers = JsonConvert.DeserializeObject<HttpHeaders>(jsonHeaders) ?? new HttpHeaders();
        
        var tokenDecoder = new JwtSecurityTokenHandler();
        var bearerToken = headers.Authorization.Replace("Bearer ", string.Empty);
        var jwtSecurityToken = (JwtSecurityToken)tokenDecoder.ReadToken(bearerToken);
        var principal = new ClaimsPrincipal(new ClaimsIdentity(jwtSecurityToken.Claims, "Bearer", "name", "role"));

        context.Features.Set(principal);
        return next(context);
    }
}