using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Shared.Status;

public class GetStatusResponse : IResponse
{
    public bool IsApiOnline { get; init; }
    public bool IsBlobsOnline { get; init; }
    public bool IsGraphExplorerOnline { get; init; }

    public GetStatusResponse() {}
    
    public GetStatusResponse(IEnumerable<string> unhealthyServices)
    {
        IsApiOnline = true;
        IsBlobsOnline = false;
        IsGraphExplorerOnline = !unhealthyServices.Contains(Services.GraphExplorer);
    }
}