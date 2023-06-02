using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Backend.Application.CurriculumVitae;

public class GetCurriculumVitaeUseCase : IGetCurriculumVitaeUseCase
{
    private readonly ICurriculumVitaeRepository _curriculumVitaeRepository;

    public GetCurriculumVitaeUseCase(ICurriculumVitaeRepository curriculumVitaeRepository)
    {
        _curriculumVitaeRepository = curriculumVitaeRepository;
    }

    public GetCurriculumVitaeResponse GetCurriculumVitae()
    {
        var curriculumVitae = _curriculumVitaeRepository.GetCurriculumVitae();
        
        return new GetCurriculumVitaeResponse()
        {
            CurriculumVitae = new CurriculumVitaeContract()
            {
                FullName = curriculumVitae.FullName
            }
        };
    }
}