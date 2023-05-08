using MadWorld.Backend.Domain.Status;
using MadWorld.Shared.Contracts.Shared.Status;

namespace MadWorld.Backend.Application.Status;

public class GetStatusUseCase : IGetStatusUseCase
{
    public GetStatusResponse GetStatus()
    {
        return new GetStatusResponse()
        {
            IsApiOnline = true
        };
    }
}