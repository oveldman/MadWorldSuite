using LanguageExt.Common;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

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

    public CurriculumVitaeContract ToContract()
    {
        return new CurriculumVitaeContract()
        {
            FullName = FullName
        };
    }
}