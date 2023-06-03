using LanguageExt;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public interface ICurriculumVitaeRepository
{
    Option<CurriculumVitae> GetCurriculumVitae();
}