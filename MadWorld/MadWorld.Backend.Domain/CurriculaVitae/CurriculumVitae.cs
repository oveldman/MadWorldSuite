using System.Net.Mime;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.General;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public sealed class CurriculumVitae
{
    public readonly Text FullName;

    private CurriculumVitae(Text fullName)
    {
        FullName = fullName;
    }
    
    public static Result<CurriculumVitae> Parse(string fullName)
    {
        var nameResult = Text.Parse(fullName);
        
        if (nameResult.IsFaulted) return new Result<CurriculumVitae>(nameResult.GetException());
        
        return new CurriculumVitae(nameResult.GetValue());
    }

    public CurriculumVitaeContract ToContract()
    {
        return new CurriculumVitaeContract()
        {
            FullName = FullName
        };
    }
}