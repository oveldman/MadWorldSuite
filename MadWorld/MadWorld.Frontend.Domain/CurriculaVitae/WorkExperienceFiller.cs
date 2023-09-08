namespace MadWorld.Frontend.Domain.CurriculaVitae;

public class WorkExperienceFiller
{
    public string Title { get; init; } = string.Empty;
    public string TimeLine { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string[] Highlights { get; init; } = Array.Empty<string>();
    public string[] TechnologyUsed { get; init; } = Array.Empty<string>();

    public required LogoFiller Logo { get; init; }
}