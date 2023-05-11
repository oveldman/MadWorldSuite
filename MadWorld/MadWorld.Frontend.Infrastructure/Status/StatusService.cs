using System.Net.Http.Json;
using MadWorld.Frontend.Application.Status;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Shared.Contracts.Shared.Status;

namespace MadWorld.Frontend.Infrastructure.Status;

public class StatusService : IStatusService
{
    private const string ApiStatusEndpoint = "GetStatus";
    
    private readonly HttpClient _anonymousClient;
    private readonly HttpClient _authorizedClient;
    
    public StatusService(IHttpClientFactory clientFactory)
    {
        _anonymousClient = clientFactory.CreateClient(ApiTypes.MadWorldApiAnonymous);
        _authorizedClient = clientFactory.CreateClient(ApiTypes.MadWorldApiAuthorizedWithoutToken);
    }

    public async Task<GetStatusResponse> GetAnonymousStatusAsync()
    {
        return await _anonymousClient.GetFromJsonAsync<GetStatusResponse>(ApiStatusEndpoint) ?? new GetStatusResponse();
    }

    public async Task<GetStatusResponse> GetAuthorizedStatusAsync()
    {
        return await _authorizedClient.GetFromJsonAsync<GetStatusResponse>(ApiStatusEndpoint) ?? new GetStatusResponse();
    }
}