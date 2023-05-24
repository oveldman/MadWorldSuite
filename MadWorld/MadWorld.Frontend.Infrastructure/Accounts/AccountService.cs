using System.Net.Http.Json;
using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Status;

namespace MadWorld.Frontend.Infrastructure.Accounts;

public class AccountService : IAccountService
{
    private readonly HttpClient _client;
    
    public AccountService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.MadWorldApiAuthorized);
    }
    
    public async Task<GetAccountsResponse> GetAccountsAsync()
    {
        return await _client.GetFromJsonAsync<GetAccountsResponse>("Account") ?? new GetAccountsResponse(Array.Empty<AccountContract>());
    }
}