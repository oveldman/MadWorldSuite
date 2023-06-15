using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Frontend.Domain.General;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using MadWorld.Shared.Contracts.Authorized.CurriculumVitae;

namespace MadWorld.Frontend.Application.CurriculaVitae;

public class PatchCurriculumVitaeUseCase : IPatchCurriculumVitaeUseCase
{
    private readonly ICurriculumVitaeService _service;

    public PatchCurriculumVitaeUseCase(ICurriculumVitaeService service)
    {
        _service = service;
    }
    
    public async Task<PatchResult> PatchCurriculumVitae(CurriculumVitaeContract curriculumVitae)
    {
        var request = CreateRequest(curriculumVitae);
        var result = await _service.PatchCurriculumVitaeAsync(request);

        return result.Match(
            _ => new PatchResult(),
            exception => new PatchResult(exception)
        );
    }

    private static PatchCurriculumVitaeRequest CreateRequest(CurriculumVitaeContract curriculumVitae)
    {
        return new PatchCurriculumVitaeRequest()
        {
            FullName = curriculumVitae.FullName,
            BirthDate = curriculumVitae.BirthDate
        };
    }
}