using System.Globalization;

namespace MadWorld.Frontend.Domain.CurriculaVitae;

public sealed class CurriculumVitaeFiller
{
    public bool NotSupported { get; init; }
    public CultureInfo? DefaultCulture { get; init; }
    
    public string DrivingLicense { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public string[] Interests { get; init; } = Array.Empty<string>();
    public LanguageFiller[] Languages { get; init; } = Array.Empty<LanguageFiller>();
    public LivingLocationFiller? LivingLocation { get; init; }
    public string Nationality { get; init; } = string.Empty;
    public string ProfileDescription { get; init; } = string.Empty;
    public WorkExperienceFiller[] WorkExperiences { get; init; } = Array.Empty<WorkExperienceFiller>();
}