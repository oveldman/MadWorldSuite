using MadWorld.Frontend.Application.Test;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorld.Frontend.UI.Shared.Pages.Test;

public partial class Ping
{
    public string AnonymousMessage { get; private set; } = string.Empty;
    public string AuthorizedMessage { get; private set; } = string.Empty;
    
    private bool Authenticated { get; set; }
    
    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    [Inject] private IPingService PingService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        Authenticated = authenticationState.User.Identity?.IsAuthenticated ?? false;

        ResetPing();
        
        await base.OnInitializedAsync();
    }

    private async Task GetPing()
    {
        AnonymousMessage = await PingService.GetAnonymousAsync();

        if (Authenticated)
        {
            AuthorizedMessage = await PingService.GetAuthorizedAsync();   
        }
        else
        {
            AuthorizedMessage = "Not authorized";
        }
    }
    
    private void ResetPing()
    {
        AnonymousMessage = "No response";
        AuthorizedMessage = "No response";
    }
}