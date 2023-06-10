using JetBrains.Annotations;
using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Suite.Pages.CurriculaVitae;

[UsedImplicitly]
public partial class CurriculumVitae
{
    private bool IsReady { get; set; }

    private CurriculumVitaeContract CurriculumVitaeContract { get; set; } = new();
    
    [Inject]
    private IGetCurriculumVitaeUseCase UseCase { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        CurriculumVitaeContract = await UseCase.GetCurriculumVitaeAsync();
        IsReady = true;
        
        await base.OnInitializedAsync();
    }
}