using LanguageExt.Common;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.System;

namespace MadWorld.Backend.Domain.Properties;

public class BirthDate : ValueObject
{
    private readonly DateTime _birthDate;
    
    private BirthDate(DateTime birthDate)
    {
        var birthDateUtc = new DateTime(
            birthDate.Year, 
            birthDate.Month, 
            birthDate.Day,
            0, 0, 0, DateTimeKind.Utc);
        _birthDate = birthDateUtc;
    }
    
    public static Result<BirthDate> Parse(DateTime birthDate)
    {
        if (IsDateInTheFuture(birthDate))
        {
            return new Result<BirthDate>(new ValidationException($"{nameof(birthDate)} cannot be in the future"));
        }

        if (IsDateOlderThan150Years(birthDate))
        {
            return new Result<BirthDate>(new ValidationException($"{nameof(birthDate)} cannot be more than 150 years ago"));
        }

        return new BirthDate(birthDate);
    }
    
    public static implicit operator DateTime(BirthDate birthDate) => birthDate._birthDate;

    public static explicit operator BirthDate(DateTime birthDate) => new(birthDate);

    private static bool IsDateInTheFuture(DateTime birthDate)
    {
        return birthDate >= SystemTime.Now();
    }
    
    private static bool IsDateOlderThan150Years(DateTime birthDate)
    {
        const int maxAge = 150;
        
        return birthDate < SystemTime
                                .Now()
                                .AddYears(-maxAge);
    }
}