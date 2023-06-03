using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Authorized.CurriculumVitae;

public class PatchCurriculumVitaeResponse : IResponse
{
    public bool Succeeded { get; private set; } = true;
}