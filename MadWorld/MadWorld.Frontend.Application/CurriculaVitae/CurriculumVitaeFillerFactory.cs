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
            Interests = new []{ "Programmeren", "Snowboarden", "Boeken lezen", "Wandelen", "Dungeons & Dragons" },
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
                                 "Ook vind ik een gezellige werksfeer erg belangrijk.",
            WorkExperiences = new []
            {
                new WorkExperienceFiller()
                {
                    Title = "Backend Developer",
                    TimeLine = "Okt. 2022 - Present",
                    Description = "Bij VI Company heb ik nieuwe API's en Azure functions opgezet en nieuwe functionaliteiten " +
                                  "ontwikkeld voor de financiële branche. Dit waren nieuwe functionaliteiten voor meer dan 10 " +
                                  "applicaties. Hier heb ik gewerkt aan projecten zoals een project voor het automatisch herbalanceren " +
                                  "van aandelen binnen de gestelde parameters. Voor andere projecten heb ik Azure-omgevingen opgezet en " +
                                  "YAML-pipelines aangelegd. ",
                    Highlights = new []
                    {
                        "Ik heb nieuwe projecten online op Azure gezet aan de hand van nieuwe pipelines, bicep-bestanden en nieuwe Azure-resources.",
                        "Nieuwe functionaliteiten ontwikkeld met de domain-driven development methode.",
                        "Dotnet-upgrades uitgevoerd van dotnet core 3.1 naar dotnet 6.0."
                    },
                    TechnologyUsed = new []{ "C#", "Kubernetes", "Azure Functions", "Rest APIs", "Devops Engineering", "MSSQL", "Bicep" },
                    Logo = new LogoFiller()
                    {
                        Title = "VI Company logo",
                        FileName = "vicompany.svg",
                        Size = "",
                        Url = "https://www.vicompany.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Fullstack Developer",
                    TimeLine = "Feb. 2020 - Okt. 2022",
                    Description = "Bij Bugs Business heb ik nieuwe API's en Azure functions opgezet en nieuwe functionaliteiten " +
                                  "ontwikkeld voor de verzekeringsbranche, zodat medewerkers eenvoudig polissen en offertes " +
                                  "kunnen beheren. Hierbij heb ik applicaties gekoppeld aan externe systemen, zodat sommige " +
                                  "polisgegevens automatisch verwerkt kunnen worden. Security is voor mij erg belangrijk, dus " +
                                  "ik denk graag mee om de beveiliging naar een hoger niveau te tillen. ",
                    Highlights = new []
                    {
                        "Ik heb nieuwe functionaliteiten ontworpen en ontwikkeld, zoals MVC-projecten en API's. ",
                        "Ik heb een aantal functionaliteiten geanalyseerd en geoptimaliseerd, waardoor de applicatie sneller is geworden.",
                        "Ik heb beveiligingsbeleid opgezet, zoals voor ISO 27001."
                    },
                    TechnologyUsed = new []{ "C#", "MVC", "Azure Functions", "Rest APIs", "Entity Framework", "MSSQL" },
                    Logo = new LogoFiller()
                    {
                        Title = "Bugs Business logo",
                        FileName = "bugsbusiness.svg",
                        Size = "15px",
                        Url = "https://www.bugsbusiness.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Stagiair - Afstuderen",
                    TimeLine = "Sept. 2018 - Jan. 2019",
                    Description = "Bij mijn afstuderen heb ik onderzocht hoe we interne applicaties het beste geautomatiseerd " +
                                  "kunnen testen op security. In mijn onderzoek heb ik gekeken naar geschikte tools voor " +
                                  "integratie, het genereren van rapporten en passende oplossingen om op beveiliging te " +
                                  "scannen. In het uiteindelijke product kon je eenvoudig de tools installeren en configureren " +
                                  "via een webapplicatie. Bij het starten van een scan werd automatisch een Docker-omgeving opgestart, " +
                                  "zodat de tools kunnen worden uitgevoerd. Het rapport kan worden ingesteld, zodat alle resultaten " +
                                  "in één overzicht worden weergegeven. Met deze resultaten controleren we of nieuwe ontwikkelingen " +
                                  "geen klassieke beveiligingsfouten bevatten. Op verzoek kan mijn scriptie worden ingezien. ",
                    Highlights = new []
                    {
                        "Ik heb geanalyseerd welke beveiligingstools we nodig hadden. ",
                        "Ik heb een applicatie ontwikkeld die ervoor zorgt dat de beveiligingstools samenwerken en de resultaten in één rapport worden weergegeven. ",
                        "Ik heb de architectuur en het ontwerp van deze applicatie ontworpen. "
                    },
                    TechnologyUsed = new []{ "C#", "MVC", "MSSQL", "Azure Functions", "Docker" },
                    Logo = new LogoFiller()
                    {
                        Title = "Bugs Business logo",
                        FileName = "bugsbusiness.svg",
                        Size = "15px",
                        Url = "https://www.bugsbusiness.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Support Developer",
                    TimeLine = "Mar. 2018 - Feb. 2020",
                    Description = "Bij de afdeling support was het mijn hoofdtaak om klantproblemen op te lossen. " +
                                  "Het analyseren van het probleem deed ik onder andere door te debuggen, logbestanden te " +
                                  "bekijken, te testen en mee te kijken met de klant. Daarnaast hielp ik regelmatig mee om " +
                                  "bugs in de code op te lossen. Ik hield een overzicht bij van deze problemen, zodat het " +
                                  "juiste team ernaar kon kijken. Ook nam ik regelmatig deel aan het scrumteam om nieuwe " +
                                  "functionaliteiten te ontwikkelen. ",
                    Highlights = new []
                    {
                        "Ik heb klantmeldingen opgepakt en problemen opgelost door configuratie of codeaanpassingen. ",
                        "Ik was het eerste aanspreekpunt voor klanten via de telefoon. ",
                        "Ik heb klantmeldingen beheerd en het overzicht bewaard. "
                    },
                    TechnologyUsed = new []{ "C#", "MVC", "Entity Framework", "MSSQL" },
                    Logo = new LogoFiller()
                    {
                        Title = "Bugs Business logo",
                        FileName = "bugsbusiness.svg",
                        Size = "15px",
                        Url = "https://www.bugsbusiness.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Stagiair - Meeloop stage",
                    TimeLine = "Sept. 2016 - Jan. 2017",
                    Description = "Tijdens mijn meeloopstage was mijn opdracht om een proces binnen het bedrijf te " +
                                  "vereenvoudigen. Na de analyse bleek dat er veel tijd werd besteed aan het beheren van " +
                                  "verschillende talen. Hiervoor heb ik een WPF-applicatie ontwikkeld om dit probleem te " +
                                  "vereenvoudigen.",
                    Highlights = new []
                    {
                        "Het analyseren en onderzoeken van processen binnen het bedrijf. ",
                        "Het ontwerpen en ontwikkelen van een nieuwe applicatie. ",
                        "Het ontwikkelen van een applicatie om alle talen te beheren voor de verschillende websites. "
                    },
                    TechnologyUsed = new []{ "C#", "WPF", "JSON" },
                    Logo = new LogoFiller()
                    {
                        Title = "VI Company logo",
                        FileName = "vicompany.svg",
                        Size = "",
                        Url = "https://www.vicompany.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Peercoach",
                    TimeLine = "Sept. 2015 - July 2016 & Feb. 2017 - July 2017",
                    Description = "Als peercoach hielp ik leerlingen om het leerproces te vergemakkelijken en zich meer " +
                                  "welkom te voelen op de hoge school. ",
                    Highlights = new []
                    {
                        "Ik heb eerstejaars studenten bijles gegeven over de vakken die ik al had gevolgd.",
                        "Ik heb geholpen bij het organiseren van open dagen.",
                        "Ik heb geholpen bij het plannen van schoolwerk voor eerstejaars studenten."
                    },
                    TechnologyUsed = new []{ "C#", "Java" },
                    Logo = new LogoFiller()
                    {
                        Title = "Hogeschool Rotterdam logo",
                        FileName = "hogeschoolrotterdam-logo.svg",
                        Size = "22",
                        Url = "https://www.hogeschoolrotterdam.nl/",
                        Title2 = "Hogeschool Rotterdam header",
                        FileName2 = "hogeschoolrotterdam-header.svg",
                        Size2 = "7",
                    }
                }
            }
        };
    }
    
    private static CurriculumVitaeFiller CreateEnglish()
    {
        return new CurriculumVitaeFiller()
        {
            DefaultCulture = DefaultCulture.English,
            DrivingLicense = "Driving License B",
            Gender = "Male",
            Interests = new []{ "Programming", "Snowboarding", "Reading Books", "Walking", "Dungeons & Dragons" },
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
                                 "friendly working atmosphere.",
            WorkExperiences = new []
            {
                new WorkExperienceFiller()
                {
                    Title = "Backend Developer",
                    TimeLine = "Okt. 2022 - Present",
                    Description = "At VI Company, I set up new APIs and Azure functions and developed new functionalities " +
                                  "for the financial sector. These were new features for more than 10 applications. " +
                                  "Here, I worked on projects such as one for automatically rebalancing stocks within " +
                                  "specified parameters. For other projects, I set up Azure environments and created YAML pipelines.",
                    Highlights = new []
                    {
                        "I deployed new projects to Azure using new pipelines, Bicep files, and new Azure resources.",
                        "I developed new functionalities using the domain-driven development method.",
                        "I performed Dotnet upgrades from dotnet core 3.1 to dotnet 6.0."
                    },
                    TechnologyUsed = new []{ "C#", "Kubernetes", "Azure Functions", "Rest APIs", "Devops Engineering", "MSSQL", "Bicep" },
                    Logo = new LogoFiller()
                    {
                        Title = "VI Company logo",
                        FileName = "vicompany.svg",
                        Size = "",
                        Url = "https://www.vicompany.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Fullstack Developer",
                    TimeLine = "Feb. 2020 - Okt. 2022",
                    Description = "At Bugs Business, I set up new APIs and Azure functions and developed new functionalities " +
                                  "for the insurance industry, making it easy for employees to manage policies and quotes. " +
                                  "I also integrated applications with external systems to enable automatic processing " +
                                  "of certain policy data. Security is crucial to me, so I am always eager to contribute " +
                                  "ideas to enhance security measures.",
                    Highlights = new []
                    {
                        "I designed and developed new functionalities such as MVC projects and APIs. ",
                        "I analyzed and optimized several functionalities, resulting in improved application performance. ",
                        "I established security policies, such as those for ISO 27001 compliance. "
                    },
                    TechnologyUsed = new []{ "C#", "MVC", "Azure Functions", "Rest APIs", "Entity Framework", "MSSQL" },
                    Logo = new LogoFiller()
                    {
                        Title = "Bugs Business logo",
                        FileName = "bugsbusiness.svg",
                        Size = "15px",
                        Url = "https://www.bugsbusiness.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Intern - Graduation",
                    TimeLine = "Sept. 2018 - Jan. 2019",
                    Description = "During my graduation project, I investigated the most effective way to automate security " +
                                  "testing for internal applications. In my research, I explored suitable tools for integration, " +
                                  "report generation, and appropriate solutions for security scanning. In the final product, " +
                                  "you could easily install and configure these tools via a web application. When initiating " +
                                  "a scan, an automated Docker environment was launched to execute the tools. The report could " +
                                  "be customized to display all results in a single overview. These results helped us verify " +
                                  "that new developments did not contain common security vulnerabilities. If desired, my thesis " +
                                  "is available for review.",
                    Highlights = new []
                    {
                        "I analyzed the security tools required. ",
                        "I developed an application that ensures the security tools collaborate and present the results in a single report. ",
                        "I designed the architecture and the layout of this application. "
                    },
                    TechnologyUsed = new []{ "C#", "MVC", "MSSQL", "Azure Functions", "Docker" },
                    Logo = new LogoFiller()
                    {
                        Title = "Bugs Business logo",
                        FileName = "bugsbusiness.svg",
                        Size = "15px",
                        Url = "https://www.bugsbusiness.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Support Developer",
                    TimeLine = "Mar. 2018 - Feb. 2020",
                    Description = "In the support department, my primary responsibility was to resolve customer issues. " +
                                  "I analyzed problems by debugging, examining log files, testing, and providing assistance " +
                                  "to customers. Additionally, I frequently contributed to resolving code bugs. I maintained " +
                                  "a record of these issues for the relevant team to address. I also actively participated in " +
                                  "the scrum team to develop new functionalities.",
                    Highlights = new []
                    {
                        "I handled customer reports and resolved issues through configuration or code adjustments.",
                        "I was the initial point of contact for customers via phone.",
                        "I managed customer reports and maintained an overview of them. "
                    },
                    TechnologyUsed = new []{ "C#", "MVC", "Entity Framework", "MSSQL" },
                    Logo = new LogoFiller()
                    {
                        Title = "Bugs Business logo",
                        FileName = "bugsbusiness.svg",
                        Size = "15px",
                        Url = "https://www.bugsbusiness.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Intern - Observational Internship",
                    TimeLine = "Sept. 2016 - Jan. 2017",
                    Description = "During my observational internship, my task was to simplify a process within the company. " +
                                  "After analysis, it was evident that a significant amount of time was dedicated to managing " +
                                  "different languages. To address this, I developed a WPF application to streamline this issue.",
                    Highlights = new []
                    {
                        "Analyzing and researching processes within the company.",
                        "Designing and developing a new application. ",
                        "Creating an application to manage all languages for various websites. "
                    },
                    TechnologyUsed = new []{ "C#", "WPF", "JSON" },
                    Logo = new LogoFiller()
                    {
                        Title = "VI Company logo",
                        FileName = "vicompany.svg",
                        Size = "",
                        Url = "https://www.vicompany.nl/"
                    }
                },
                new WorkExperienceFiller()
                {
                    Title = "Peercoach",
                    TimeLine = "Sept. 2015 - July 2016 & Feb. 2017 - July 2017",
                    Description = "As a peer coach, I assisted students in facilitating the learning process and making " +
                                  "them feel more welcome at the high school.",
                    Highlights = new []
                    {
                        "I provided tutoring to first-year students in subjects I had already completed. ",
                        "I assisted in organizing open days. ",
                        "I helped plan schoolwork schedules for first-year students. "
                    },
                    TechnologyUsed = new []{ "C#", "Java" },
                    Logo = new LogoFiller()
                    {
                        Title = "Hogeschool Rotterdam logo",
                        FileName = "hogeschoolrotterdam-logo.svg",
                        Size = "22",
                        Url = "https://www.hogeschoolrotterdam.nl/",
                        Title2 = "Hogeschool Rotterdam header",
                        FileName2 = "hogeschoolrotterdam-header.svg",
                        Size2 = "7",
                    }
                }
            }
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