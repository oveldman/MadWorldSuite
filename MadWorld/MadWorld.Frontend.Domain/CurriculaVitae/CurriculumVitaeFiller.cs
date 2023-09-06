using System.Globalization;

namespace MadWorld.Frontend.Domain.CurriculaVitae;

public sealed class CurriculumVitaeFiller
{
    public bool NotSupported { get; init; }
    public CultureInfo? DefaultCulture { get; init; }
    
    public string DrivingLicense { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public LivingLocationFiller? LivingLocation { get; init; }
    public string Nationality { get; init; } = string.Empty;
}