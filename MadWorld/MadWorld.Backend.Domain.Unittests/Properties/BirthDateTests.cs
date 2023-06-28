using AutoFixture.Xunit2;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Domain.System;
using MadWorld.Unittests.Types;
using Shouldly;

namespace MadWorld.Backend.Domain.Unittests.Properties;

[Collection(CollectionTypes.StaticSystemTimes)]
public class BirthDateTests
{
    [Fact]
    public void Parse_ValidBirthDate_ShouldReturnBirthDate()
    {
        // Arrange
        var dateToday = new DateTime(2000, 05, 05);
        SystemTime.SetDateTime(dateToday);
        var birthDate = new DateTime(1990, 10, 10);
        
        // Act
        var birthDateParsed = BirthDate.Parse(birthDate);
        
        // Assert
        birthDateParsed.IsSuccess.ShouldBe(true);
        DateTime birthDateValue = birthDateParsed.GetValue();
        birthDateValue.ShouldBe(birthDate);
        
        // Cleanup
        SystemTime.ResetDateTime();
    }
    
    [Fact]
    public void Parse_WhenBirthDateInFuture_ShouldReturnException()
    {
        // Arrange
        var dateToday = new DateTime(2020, 05, 05);
        SystemTime.SetDateTime(dateToday);
        var birthDate = new DateTime(2025, 10, 10);
        
        // Act
        var birthDateParsed = BirthDate.Parse(birthDate);
        
        // Assert
        birthDateParsed.IsSuccess.ShouldBe(false);
        birthDateParsed.GetException().ShouldBeOfType<ValidationException>();

        // Cleanup
        SystemTime.ResetDateTime();
    }
    
    [Theory]
    [InlineAutoData(1900, 05)]
    [InlineAutoData(2000, 05)]
    [InlineAutoData(2050, 04)]
    public void Parse_WhenBirthDateIsTooOld_ShouldReturnException(int year, int month)
    {
        // Arrange
        var dateToday = new DateTime(2200, 05, 05);
        SystemTime.SetDateTime(dateToday);
        var birthDate = new DateTime(year, month, 10);
        
        // Act
        var birthDateParsed = BirthDate.Parse(birthDate);
        
        // Assert
        birthDateParsed.IsSuccess.ShouldBe(false);
        birthDateParsed.GetException().ShouldBeOfType<ValidationException>();

        // Cleanup
        SystemTime.ResetDateTime();
    }
}