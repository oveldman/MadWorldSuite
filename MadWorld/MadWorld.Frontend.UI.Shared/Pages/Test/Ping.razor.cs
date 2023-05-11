using MadWorld.Frontend.Application.Test;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MadWorld.Frontend.UI.Shared.Pages.Test;

public partial class Ping
{
    private string AnonymousMessage { get; set; } = string.Empty;
    private string AuthorizedMessage { get; set; } = string.Empty;
    private bool IsAuthenticated { get; set; }
    private bool IsDisabled { get; set; }

    [Inject] public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;
    [Inject] private IPingService PingService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        IsAuthenticated = authenticationState.User.Identity?.IsAuthenticated ?? false;

        ResetPing();
        
        await base.OnInitializedAsync();
    }

    private async Task GetPing()
    {
        IsDisabled = true;
        StateHasChanged();
        
        var anonymousPingTask = GetAnonymousPing();
        var authorizedPingTask = GetAuthorizedPing();
        await Task.WhenAll(anonymousPingTask, authorizedPingTask);

        IsDisabled = false;
        StateHasChanged();
    }

    private async Task GetAnonymousPing()
    {
        AnonymousMessage = await PingService.GetAnonymousAsync();
    }
    
    private async Task GetAuthorizedPing()
    {
        if (IsAuthenticated)
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