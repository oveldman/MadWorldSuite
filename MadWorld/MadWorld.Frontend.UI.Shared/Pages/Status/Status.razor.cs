using JetBrains.Annotations;
using MadWorld.Frontend.Application.Status;
using MadWorld.Shared.Contracts.Shared.Status;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Shared.Pages.Status;

[UsedImplicitly]
public sealed  partial class Status
{
    private bool IsReady { get; set; }
    private GetStatusResponse AnonymousStatus { get; set; } = new();
    private GetStatusResponse AuthorizedStatus { get; set; } = new();
    
    [Inject] private IStatusService StatusService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        AnonymousStatus = await StatusService.GetAnonymousStatusAsync();
        AuthorizedStatus = await StatusService.GetAuthorizedStatusAsync();
        IsReady = true;
        
        await base.OnInitializedAsync();
    }

    private static string GetOnlineClass(bool isOnline)
    {
        return isOnline ? "is-online" : "is-offline";
    }
}