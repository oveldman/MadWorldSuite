using JetBrains.Annotations;
using MadWorld.Frontend.Application.CurriculaVitae;
using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Suite.Pages.CurriculaVitae;

[UsedImplicitly]
public partial class CurriculumVitae
{
    [Parameter]
    public string? Language { get; set; }
    
    private bool IsReady { get; set; }

    private CurriculumVitaeContract CurriculumVitaeContract { get; set; } = new();
    private CurriculumVitaeFiller CurriculumVitaeFiller { get; set; } = new();
    
    [Inject]
    private IGetCurriculumVitaeUseCase UseCase { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Language ??= "nl";
        
        CurriculumVitaeContract = await UseCase.GetCurriculumVitaeAsync();
        CurriculumVitaeFiller = CurriculumVitaeFillerFactory.Create(Language);
        IsReady = true;
        
        await base.OnInitializedAsync();
    }
}