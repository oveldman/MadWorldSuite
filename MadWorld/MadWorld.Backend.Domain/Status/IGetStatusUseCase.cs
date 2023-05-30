using MadWorld.Shared.Contracts.Shared.Status;

namespace MadWorld.Backend.Domain.Status;

public interface IGetStatusUseCase
{
    Task<GetStatusResponse> GetStatus();
}