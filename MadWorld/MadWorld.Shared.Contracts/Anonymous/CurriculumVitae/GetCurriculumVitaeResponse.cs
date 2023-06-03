using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;

public sealed class GetCurriculumVitaeResponse : IResponse
{
    public CurriculumVitaeContract CurriculumVitae { get; set; } = new();
}