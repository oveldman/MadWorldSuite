using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Shared.Status;

public class GetStatusResponse : IResponse
{
    public bool IsApiOnline { get; init; } = false;
    public bool IsBlobsOnline { get; init; } = false;
}