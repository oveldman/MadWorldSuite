using System.Security.Claims;

namespace MadWorld.Backend.API.Shared.Authorization;

public static class ClaimsPrincipalExtensions
{
    public static string GetClaimValue(this ClaimsPrincipal principal, string name)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == name)?.Value ?? string.Empty;
    }
}