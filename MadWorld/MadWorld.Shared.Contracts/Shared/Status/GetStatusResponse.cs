namespace MadWorld.Shared.Contracts.Shared.Status;

public class GetStatusResponse
{
    public bool IsApiOnline { get; init; } = false;
    public bool IsBlobsOnline { get; init; } = false;
}