using MadWorld.Frontend.Application.Test;
using MadWorld.Frontend.Domain.Api;

namespace MadWorld.Frontend.Infrastructure.Test;

public class PingService : IPingService
{
    private readonly HttpClient _anonymousClient;
    private readonly HttpClient _authorizedClient;
    
    public PingService(IHttpClientFactory clientFactory)
    {
        _anonymousClient = clientFactory.CreateClient(ApiTypes.MadWorldApiAnonymous);
        _authorizedClient = clientFactory.CreateClient(ApiTypes.MadWorldApiAuthorized);
    }

    public async Task<string> GetAnonymousAsync()
    {
        return await _anonymousClient.GetStringAsync("Ping");
    }

    public async Task<string>  GetAuthorizedAsync()
    {
        return await _authorizedClient.GetStringAsync("Ping");
    }
}