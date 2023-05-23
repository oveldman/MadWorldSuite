using System.Security.Claims;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace MadWorld.Frontend.UI.Shared.Dependencies;

public class AccountExtraClaimsPrincipalFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public AccountExtraClaimsPrincipalFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
    {
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account,
        RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        if (!(user?.Identity?.IsAuthenticated ?? false)) return user ?? new ClaimsPrincipal();
        
        var claimsIdentity = (ClaimsIdentity)user.Identity;
        var claimRoles = claimsIdentity.Claims
            .FirstOrDefault(c => c.Type == ClaimNames.Role, new Claim(ClaimNames.Role, "None"))
            .Value;

        AddRoles(claimsIdentity, claimRoles);

        return user;
    }
    
    private void AddRoles(ClaimsIdentity claimsIdentity, string claimRoles)
    {
        var roles = claimRoles.Split(';');
        foreach (var role in roles)
        {
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
        }
    }
}