using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Frontend.Domain.General;

namespace MadWorld.Frontend.Application.CurriculaVitae;

public static class CurriculumVitaeFillerFactory
{
    public static CurriculumVitaeFiller Create(string language)
    {
        return language switch
        {
            "en" => CreateEnglish(),
            "nl" => CreateDutch(),
            _ => CreateNotSupported()
        };
    }
    
    private static CurriculumVitaeFiller CreateDutch()
    {
        return new CurriculumVitaeFiller()
        {
            DefaultCulture = DefaultCulture.Dutch,
            DrivingLicense = "Rijbewijs B",
            Gender = "Man",
            LivingLocation = new LivingLocationFiller
            {
                Address = "Adres 1",
                City = "Plaats",
                PostalCode = "Postcode"
            },
            Nationality = " Nederlands"
        };
    }
    
    private static CurriculumVitaeFiller CreateEnglish()
    {
        return new CurriculumVitaeFiller()
        {
            DefaultCulture = DefaultCulture.English,
            DrivingLicense = "Driving License B",
            Gender = "Male",
            LivingLocation = new LivingLocationFiller
            {
                Address = "Address 1",
                City = "City",
                PostalCode = "Postal Code"
            },
            Nationality = " Dutch"
        };
    }
    
    private static CurriculumVitaeFiller CreateNotSupported()
    {
        return new CurriculumVitaeFiller
        {
            NotSupported = true
        };
    }
}