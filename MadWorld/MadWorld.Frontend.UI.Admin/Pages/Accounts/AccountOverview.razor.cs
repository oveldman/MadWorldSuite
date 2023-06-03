using JetBrains.Annotations;
using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Shared.Contracts.Authorized.Account;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MadWorld.Frontend.UI.Admin.Pages.Accounts;

[UsedImplicitly]
public sealed partial class AccountOverview
{
    private bool IsReady { get; set; }
    
    private IReadOnlyCollection<AccountContract> Accounts { get; set; } = Array.Empty<AccountContract>();
    
    [Inject] private IGetAccountsUseCase GetAccountsUseCase { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Accounts = await GetAccountsUseCase.GetAccountsAsync();
        IsReady = true;
        
        await base.OnInitializedAsync();
    }

    private void OpenAccount(DataGridRowMouseEventArgs<AccountContract> args)
    {
        NavigationManager.NavigateTo($"/Account/{args.Data.Id}");
    }
}