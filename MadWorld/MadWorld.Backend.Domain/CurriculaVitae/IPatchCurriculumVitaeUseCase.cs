using LanguageExt.Common;
using MadWorld.Shared.Contracts.Authorized.CurriculumVitae;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public interface IPatchCurriculumVitaeUseCase
{
    Result<PatchCurriculumVitaeResponse> PatchCurriculumVitae(PatchCurriculumVitaeRequest request);
}