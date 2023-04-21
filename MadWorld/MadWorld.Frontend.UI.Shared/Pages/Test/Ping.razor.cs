using MadWorld.Frontend.Application.Test;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Shared.Pages.Test;

public partial class Ping
{
    public string AnonymousMessage { get; private set; } = string.Empty;
    public string AuthorizedMessage { get; private set; } = string.Empty;
    
    [Inject] private IPingService PingService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        AnonymousMessage = "No response";
        AuthorizedMessage = "No response";
        
        await base.OnInitializedAsync();
    }

    public async Task GetPing()
    {
        AnonymousMessage = await PingService.GetAnonymousAsync();
        AuthorizedMessage = await PingService.GetAuthorizedAsync();
    }
}