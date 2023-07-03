namespace MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

public sealed class CurriculumVitaeContract
{
    public DateTime BirthDate { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}