using LanguageExt.Common;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Domain.System;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

namespace MadWorld.Backend.Domain.CurriculaVitae;

public sealed class CurriculumVitae : IValueObject
{
    public readonly BirthDate BirthDate;
    public readonly Text FullName;
    public readonly Text Title;
    
    [RepositoryPublicOnly]
    public CurriculumVitae(BirthDate birthDate, Text fullName, Text title)
    {
        FullName = fullName;
        BirthDate = birthDate;
        Title = title;
    }

    public static Result<CurriculumVitae> Parse(DateTime birthDate, string fullName, string title)
    {
        var birthDateResult = BirthDate.Parse(birthDate);
        var nameResult = Text.Parse(fullName);
        var titleResult = Text.Parse(title);

        
        if (Result.HasFaultyState(
                out var exception,
                birthDateResult.GetValueObjectResult(),
                nameResult.GetValueObjectResult(),
                titleResult.GetValueObjectResult()
            ))
        {
            return new Result<CurriculumVitae>(exception);
        }

        return new CurriculumVitae(
            birthDateResult.GetValue(),
            nameResult.GetValue(),
            titleResult.GetValue()
        );
    }

    public CurriculumVitaeContract ToContract()
    {
        return new CurriculumVitaeContract()
        {
            FullName = FullName,
            BirthDate = BirthDate,
            Title = Title
        };
    }
}