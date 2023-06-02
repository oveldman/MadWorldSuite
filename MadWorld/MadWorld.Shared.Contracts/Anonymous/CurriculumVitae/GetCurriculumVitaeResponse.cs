namespace MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

public class GetCurriculumVitaeResponse
{
    public CurriculumVitaeContract CurriculumVitae { get; set; } = new();
}