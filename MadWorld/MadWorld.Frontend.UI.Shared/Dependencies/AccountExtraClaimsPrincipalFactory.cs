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

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);

        if (user?.Identity?.IsAuthenticated ?? false)
        {
            var claimsIdentity = (ClaimsIdentity)user.Identity;
            var roles = claimsIdentity
                .Claims.FirstOrDefault(c => c.Type == ClaimNames.Role)?.Value ?? "None";
            
            foreach (var role in roles.Split(";"))
            {
                claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
        }
        
        return user ?? new ClaimsPrincipal();
    }
}