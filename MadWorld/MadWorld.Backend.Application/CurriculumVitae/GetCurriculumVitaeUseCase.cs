using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Backend.Application.CurriculumVitae;

public sealed class GetCurriculumVitaeUseCase : IGetCurriculumVitaeUseCase
{
    private readonly ICurriculumVitaeRepository _curriculumVitaeRepository;

    public GetCurriculumVitaeUseCase(ICurriculumVitaeRepository curriculumVitaeRepository)
    {
        _curriculumVitaeRepository = curriculumVitaeRepository;
    }

    public Option<GetCurriculumVitaeResponse> GetCurriculumVitae()
    {
        var curriculumVitaeOption = _curriculumVitaeRepository.GetCurriculumVitae();

        if (curriculumVitaeOption.IsNone)
        {
            return Option<GetCurriculumVitaeResponse>.None;
        }
        
        return new GetCurriculumVitaeResponse()
        {
            CurriculumVitae = new CurriculumVitaeContract()
            {
                FullName = curriculumVitaeOption.ValueUnsafe().FullName
            }
        };
    }
}