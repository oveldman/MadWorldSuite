namespace MadWorld.Shared.Contracts.Authorized.CurriculumVitae;

public class PatchCurriculumVitaeRequest
{
    public DateTime BirthDate { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
}