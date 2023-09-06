using JetBrains.Annotations;
using MadWorld.Frontend.Application.CurriculaVitae;
using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Suite.Pages.CurriculaVitae;

[UsedImplicitly]
public partial class CurriculumVitae
{
    private const string DefaultLanguage = "nl";
    
    [Parameter]
    public string? Language { get; set; }
    
    private bool IsReady { get; set; }

    private CurriculumVitaeContract CurriculumVitaeContract { get; set; } = new();
    private CurriculumVitaeFiller CurriculumVitaeFiller { get; set; } = new();
    
    [Inject]
    private IGetCurriculumVitaeUseCase UseCase { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        Language ??= DefaultLanguage;
        
        CurriculumVitaeContract = await UseCase.GetCurriculumVitaeAsync();
        CurriculumVitaeFiller = CurriculumVitaeFillerFactory.Create(Language);
        IsReady = true;
        
        await base.OnInitializedAsync();
    }

    protected override void OnParametersSet()
    {
        Language ??= DefaultLanguage;
        
        CurriculumVitaeFiller = CurriculumVitaeFillerFactory.Create(Language);
        
        base.OnParametersSet();
    }
}