using LanguageExt;
using LanguageExt.Common;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public interface ICurriculumVitaeRepository
{
    Option<CurriculumVitae> GetCurriculumVitae();
    Result<bool> UpdateCurriculumVitae(CurriculumVitae curriculumVitae);
}