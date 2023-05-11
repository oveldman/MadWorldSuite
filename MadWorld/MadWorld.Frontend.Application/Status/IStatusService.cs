using MadWorld.Shared.Contracts.Shared.Status;

namespace MadWorld.Frontend.Application.Status;

public interface IStatusService
{
    Task<GetStatusResponse> GetAnonymousStatusAsync();
    Task<GetStatusResponse> GetAuthorizedStatusAsync();
}