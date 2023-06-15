namespace MadWorld.Shared.Contracts.Authorized.CurriculumVitae;

public class PatchCurriculumVitaeRequest
{
    public string FullName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
}