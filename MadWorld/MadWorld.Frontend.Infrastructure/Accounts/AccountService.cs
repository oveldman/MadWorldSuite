using System.Net.Http.Json;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Frontend.Domain.Accounts;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Error;

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
    
    public async Task<Result<PatchAccountResponse>> PatchAccountAsync(PatchAccountRequest request)
    {
        var response = await _client.PatchAsJsonAsync($"{Endpoint}", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PatchAccountResponse>() ?? new();   
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>() ?? ErrorResponse.CreateDefault();
        return new Result<PatchAccountResponse>(new ApiResponseException(errorResponse.Message));
    }
}