using MadWorld.Frontend.Domain.General;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Frontend.Domain.CurriculaVitae;

public interface IPatchCurriculumVitaeUseCase
{
    Task<PatchResult> PatchCurriculumVitae(CurriculumVitaeContract curriculumVitae);
}