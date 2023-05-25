using JetBrains.Annotations;
using LanguageExt;
using LanguageExt.SomeHelp;
using MadWorld.Frontend.Domain.Accounts;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Admin.Pages.Accounts;

[UsedImplicitly]
public partial class AccountDetails
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    private bool IsReady { get; set; }

    private Option<Account> AccountOption { get; set; } = Option<Account>.None;

    private Account Account => AccountOption.Match<Account>(
        a => a,
        () => throw new NullReferenceException($"{nameof(Account)} cannot be null"));

    [Inject] private IGetAccountUseCase GetAccountUseCase { get; set; } = null!;
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        AccountOption = await GetAccountUseCase.GetAccountAsync(Id);
        IsReady = true;
        
        await base.OnInitializedAsync();
    }

    private void SaveChanges(Account account)
    {
        
    }

    private void CancelChanges()
    {
        NavigationManager.NavigateTo("/Accounts");
    }
}