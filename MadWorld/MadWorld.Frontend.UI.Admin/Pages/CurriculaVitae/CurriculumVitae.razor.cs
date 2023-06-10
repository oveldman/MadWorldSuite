using JetBrains.Annotations;
using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Admin.Pages.CurriculaVitae;

[UsedImplicitly]
public partial class CurriculumVitae
{
    private bool IsReady { get; set; }
    private bool IsSaved { get; set; }
    private bool HasError { get; set; }
    private string ErrorMessage { get; set; } = string.Empty;
    
    private CurriculumVitaeContract CurriculumVitaeContract { get; set; } = new();
    
    [Inject] private IGetCurriculumVitaeUseCase GetCurriculumVitaeUseCase { get; set; } = null!;
    [Inject] private IPatchCurriculumVitaeUseCase PatchCurriculumVitaeUseCase { get; set; } = null!;
    
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        CurriculumVitaeContract = await GetCurriculumVitaeUseCase.GetCurriculumVitaeAsync();
        IsReady = true;
        
        await base.OnInitializedAsync();
    }

    private async Task SaveChanges(CurriculumVitaeContract curriculumVitae)
    {
        Reset();
        var result = await PatchCurriculumVitaeUseCase.PatchCurriculumVitae(curriculumVitae);
        
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
        NavigationManager.NavigateTo("/");
    }
    
    private void Reset()
    {
        IsSaved = false;
        HasError = false;
        ErrorMessage = string.Empty;
    }
}