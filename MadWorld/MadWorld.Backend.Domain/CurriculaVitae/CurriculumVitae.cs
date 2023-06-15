using LanguageExt.Common;
using MadWorld.Backend.Domain.General;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public sealed class CurriculumVitae : ValueObject
{
    public readonly Text FullName;
    public readonly BirthDate BirthDate;

    private CurriculumVitae(Text fullName, BirthDate birthDate)
    {
        FullName = fullName;
        BirthDate = birthDate;
    }

    public static Result<CurriculumVitae> Parse(string fullName, DateTime birthDate)
    {
        var nameResult = Text.Parse(fullName);
        var birthDateResult = BirthDate.Parse(birthDate);
        
        if (Result.HasFaultyState(
                out var exception,
                nameResult.GetValueObjectResult(), 
                birthDateResult.GetValueObjectResult()
            ))
        {
            return new Result<CurriculumVitae>(exception);
        }
        
        return new CurriculumVitae(
            nameResult.GetValue(),
            birthDateResult.GetValue()
            );
    }

    public CurriculumVitaeContract ToContract()
    {
        return new CurriculumVitaeContract()
        {
            FullName = FullName,
            BirthDate = BirthDate
        };
    }
}