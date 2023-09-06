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
            Interests = new []{ "Programmeren", "Snowboarden", "Boeken lezen", "Wandelen", "Dungeons & Dragon" },
            Languages = new []
            {
                new LanguageFiller { Name = "Nederlands", Level = "Moedertaal" },
                new LanguageFiller { Name = "Engels", Level = "Professioneel" }
            },
            LivingLocation = new LivingLocationFiller
            {
                Address = "Adres 1",
                City = "Plaats",
                PostalCode = "Postcode"
            },
            Nationality = " Nederlands",
            ProfileDescription = "Mijn naam is Oscar, een flexibele, oplossingsgerichte, doorzettende developer met 5 jaar " +
                                 "werkervaring in de ICT-branche. Daarnaast ben ik een teamspeler en help ik graag collega's. " +
                                 "Ik zoek naar een baan waar ik werk aan innovatieve en nieuwe technieken in backend applicaties " +
                                 "met nieuwe uitdagingen. Ik doe graag aan het beveiligen van applicaties en blijf graag leren. " +
                                 "Ook vind ik een gezellige werksfeer erg belangrijk."
        };
    }
    
    private static CurriculumVitaeFiller CreateEnglish()
    {
        return new CurriculumVitaeFiller()
        {
            DefaultCulture = DefaultCulture.English,
            DrivingLicense = "Driving License B",
            Gender = "Male",
            Interests = new []{ "Programming", "Snowboarding", "Reading Books", "Walking", "Dungeons & Dragon" },
            Languages = new []
            {
                new LanguageFiller { Name = "Dutch", Level = "Native" }, 
                new LanguageFiller { Name = "English", Level = "Professional" }
            },
            LivingLocation = new LivingLocationFiller
            {
                Address = "Address 1",
                City = "City",
                PostalCode = "Postal Code"
            },
            Nationality = " Dutch",
            ProfileDescription = "My name is Oscar, a flexible, solution-oriented, persistent developer with 5 years of " +
                                 "work experience in the IT industry. Additionally, I am a team player and enjoy assisting " +
                                 "colleagues. I am seeking a job where I can work on innovative and emerging technologies " +
                                 "in backend applications, tackling new challenges. I have a keen interest in securing " +
                                 "applications and am committed to continuous learning. Furthermore, I highly value a " +
                                 "friendly working atmosphere."
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