using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.Azure.Functions.Worker;

namespace MadWorld.Backend.API.Shared.Authorization;

public static class FunctionContextExtensions
{
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