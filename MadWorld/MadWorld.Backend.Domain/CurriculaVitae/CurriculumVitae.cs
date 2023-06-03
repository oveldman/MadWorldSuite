using LanguageExt.Common;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public sealed class CurriculumVitae
{
    public string FullName { get; set; } = string.Empty;

    private CurriculumVitae() {}
    
    public static Result<CurriculumVitae> Parse(string fullName)
    {
        return new CurriculumVitae()
        {
            FullName = fullName
        };
    }
}