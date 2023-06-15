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
            request.FullName, 
            request.BirthDate
            );

        if (curriculumVitaeResult.IsFaulted) 
            return new Result<PatchCurriculumVitaeResponse>(curriculumVitaeResult.GetException());

        var result = _repository.UpdateCurriculumVitae(curriculumVitaeResult.GetValue());
        
        if (result.IsFaulted) 
            return new Result<PatchCurriculumVitaeResponse>(result.GetException());

        return new PatchCurriculumVitaeResponse();
    }
}