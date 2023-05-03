using System.Security.Authentication;
using System.Security.Claims;
using MadWorld.Backend.API.Shared.Functions;
using Microsoft.Azure.Functions.Worker;

namespace MadWorld.Backend.API.Shared.Authorization;

public static class FunctionContextExtensions
{
    private static readonly IReadOnlyCollection<string> AnonymousEndpoints = new List<string>()
    {
        nameof(HealthCheck),
        "RenderOAuth2Redirect",
        "RenderOpenApiDocument",
        "RenderSwaggerDocument",
        "RenderSwaggerUI",
    };

    public static bool IsEndpointAnonymous(this FunctionContext context)
    {
        var azureFunctionName = context.FunctionDefinition.Name;
        return AnonymousEndpoints.Contains(azureFunctionName);
    }
    
    public static User GetUser(this FunctionContext context)
    {
        var claimsPrincipal = context.Features.Get<ClaimsPrincipal>();

        if (claimsPrincipal == null)
        {
            throw new AuthenticationException("User not found.");
        }
        
        return new User()
        {
            Id = claimsPrincipal.GetClaimValue("oid"),
            Name = claimsPrincipal.GetClaimValue("name"),
            Email = claimsPrincipal.GetClaimValue("emails")
        };
    }
}