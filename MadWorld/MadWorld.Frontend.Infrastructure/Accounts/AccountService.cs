using System.Net.Http.Json;
using LanguageExt;
using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Frontend.Infrastructure.Accounts;

public class AccountService : IAccountService
{
    private const string Endpoint = "Account";
    
    private readonly HttpClient _client;
    
    public AccountService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.MadWorldApiAuthorized);
    }
    
    public async Task<GetAccountsResponse> GetAccountsAsync()
    {
        return await _client.GetFromJsonAsync<GetAccountsResponse>(Endpoint) ?? new GetAccountsResponse(Array.Empty<AccountContract>());
    }
    
    public async Task<Option<GetAccountResponse>> GetAccountAsync(string id)
    {
        var response = await _client.GetAsync($"{Endpoint}/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return new Option<GetAccountResponse>();
        }

        return await response.Content.ReadFromJsonAsync<GetAccountResponse>();
    }
}