using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Backend.Application.CurriculaVitae;

public sealed class GetCurriculumVitaeUseCase : IGetCurriculumVitaeUseCase
{
    private readonly ICurriculumVitaeRepository _repository;

    public GetCurriculumVitaeUseCase(ICurriculumVitaeRepository repository)
    {
        _repository = repository;
    }

    public Option<GetCurriculumVitaeResponse> GetCurriculumVitae()
    {
        var curriculumVitaeOption = _repository.GetCurriculumVitae();

        if (curriculumVitaeOption.IsNone)
        {
            return Option<GetCurriculumVitaeResponse>.None;
        }
        
        return new GetCurriculumVitaeResponse()
        {
            CurriculumVitae = curriculumVitaeOption
                                .ValueUnsafe()
                                .ToContract()
        };
    }
}