using LanguageExt;
using LanguageExt.Common;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using MadWorld.Shared.Contracts.Authorized.CurriculumVitae;

namespace MadWorld.Frontend.Domain.CurriculaVitae;

public interface ICurriculumVitaeService
{
    Task<Option<GetCurriculumVitaeResponse>> GetCurriculumVitaeAsync();
    Task<Result<PatchCurriculumVitaeResponse>> PatchCurriculumVitaeAsync(PatchCurriculumVitaeRequest request);
}