using MadWorld.Frontend.Domain.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Options;

namespace MadWorld.Frontend.UI.Shared.Security;

public class SuiteAuthorizedMessageHandler : AuthorizationMessageHandler
{
    public SuiteAuthorizedMessageHandler(IAccessTokenProvider provider, 
        NavigationManager navigation,
        IOptions<ApiUrls> apiUrlsOption)
        : base(provider, navigation)
    {
        ConfigureHandler(
            authorizedUrls: new[] { apiUrlsOption.Value.BaseUrlAuthorized },
            scopes: new[] {
                "https://nlMadWorld.onmicrosoft.com/4605ec9b-98b5-411b-b98b-d0a784221487/API.Access"
            });
    }
}