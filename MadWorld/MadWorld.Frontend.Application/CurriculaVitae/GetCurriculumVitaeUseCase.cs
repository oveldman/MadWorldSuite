using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Frontend.Application.CurriculaVitae;

public class GetCurriculumVitaeUseCase : IGetCurriculumVitaeUseCase
{
    private readonly ICurriculumVitaeService _service;

    public GetCurriculumVitaeUseCase(ICurriculumVitaeService service)
    {
        _service = service;
    }
    
    public async Task<CurriculumVitaeContract> GetCurriculumVitaeAsync()
    {
        var response = await _service.GetCurriculumVitaeAsync();

        return response.Match(
            r => r.CurriculumVitae,
            () => new CurriculumVitaeContract()
        );
    }
}