using JetBrains.Annotations;
using LanguageExt;
using MadWorld.Frontend.Domain.Accounts;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Admin.Pages.Accounts;

[UsedImplicitly]
public partial class AccountDetails
{
    [Parameter]
    public string Id { get; set; } = string.Empty;

    private bool IsReady { get; set; }
    private bool IsSaved { get; set; }
    private bool HasError { get; set; }
    private string ErrorMessage { get; set; } = string.Empty;

    private Option<Account> AccountOption { get; set; } = Option<Account>.None;

    private Account Account => AccountOption.Match<Account>(
        a => a,
        () => throw new NullReferenceException($"{nameof(Account)} cannot be null"));

    [Inject] private IGetAccountUseCase GetAccountUseCase { get; set; } = null!;
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    [Inject] private IPatchAccountUseCase PatchAccountUseCase { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        AccountOption = await GetAccountUseCase.GetAccountAsync(Id);
        IsReady = true;
        
        await base.OnInitializedAsync();
    }

    private async Task SaveChanges(Account account)
    {
        Reset();
        var result = await PatchAccountUseCase.PatchAccount(account);

        if (result.IsSuccess)
        {
            IsSaved = true;
        }
        else
        {
            HasError = true;
            ErrorMessage = result.ErrorMessage;
        }
    }

    private void CancelChanges()
    {
        NavigationManager.NavigateTo("/Accounts");
    }
    
    private void AdminCheckBoxChanged(bool isChecked)
    {
        if (isChecked)
        {
            Account.HasUserRole = true;
        }
    }

    public void UserCheckBoxChanged(bool isChecked)
    {
        if (!isChecked)
        {
            Account.HasAdminRole = false;
        }
    }
    
    private void Reset()
    {
        IsSaved = false;
        HasError = false;
        ErrorMessage = string.Empty;
    }
}