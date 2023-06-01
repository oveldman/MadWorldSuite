using System.Security.Authentication;
using System.Security.Claims;
using MadWorld.Shared.Contracts.Shared.Authorization;

namespace MadWorld.Backend.API.Shared.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static User GetUser(this ClaimsPrincipal? claimsPrincipal)
    {
        if (claimsPrincipal == null)
        {
            throw new AuthenticationException("User not found.");
        }
        
        return new User()
        {
            Id = claimsPrincipal.GetClaimValue("oid"),
            Name = claimsPrincipal.GetClaimValue("name"),
            Email = claimsPrincipal.GetClaimValue("emails"),
            Roles = claimsPrincipal.GetClaimValue(ClaimNames.Role)
        };
    }
    
    private static string GetClaimValue(this ClaimsPrincipal principal, string name)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == name)?.Value ?? string.Empty;
    }
}