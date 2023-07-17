using LanguageExt.Common;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Authorized.CurriculumVitae;

namespace MadWorld.Backend.Application.CurriculaVitae;

public class PatchCurriculumVitaeUseCase : IPatchCurriculumVitaeUseCase
{
    private readonly ICurriculumVitaeRepository _repository;

    public PatchCurriculumVitaeUseCase(ICurriculumVitaeRepository repository)
    {
        _repository = repository;
    }
    
    public Result<PatchCurriculumVitaeResponse> PatchCurriculumVitae(PatchCurriculumVitaeRequest request)
    {
        var curriculumVitaeResult = CurriculumVitae.Parse(
            request.BirthDate,
            request.FullName,
            request.Title
        );

        return curriculumVitaeResult.Match(
            PatchCurriculumVitae,
            _ => new Result<PatchCurriculumVitaeResponse>(curriculumVitaeResult.GetException())
        );
    }
    
    private Result<PatchCurriculumVitaeResponse> PatchCurriculumVitae(CurriculumVitae curriculumVitae)
    {
        var result = _repository.UpdateCurriculumVitae(curriculumVitae);

        return result.Match(
            _ => new PatchCurriculumVitaeResponse(),
            _ => new Result<PatchCurriculumVitaeResponse>(result.GetException())
        );
    }
}