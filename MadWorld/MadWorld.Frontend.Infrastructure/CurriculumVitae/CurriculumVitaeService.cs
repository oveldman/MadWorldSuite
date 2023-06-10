using System.Net.Http.Json;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Frontend.Domain.CurriculaVitae;
using MadWorld.Shared.Contracts.Anonymous.CurriculumVitae;
using MadWorld.Shared.Contracts.Authorized.CurriculumVitae;
using MadWorld.Shared.Contracts.Shared.Error;

namespace MadWorld.Frontend.Infrastructure.CurriculumVitae;

public class CurriculumVitaeService : ICurriculumVitaeService
{
    private const string Endpoint = "CurriculumVitae";
    
    private readonly HttpClient _anonymousClient;
    private readonly HttpClient _authorizedclient;
    
    public CurriculumVitaeService(IHttpClientFactory clientFactory)
    {
        _anonymousClient = clientFactory.CreateClient(ApiTypes.MadWorldApiAnonymous);
        _authorizedclient = clientFactory.CreateClient(ApiTypes.MadWorldApiAuthorized);
    }
    
    public async Task<Option<GetCurriculumVitaeResponse>> GetCurriculumVitaeAsync()
    {
        var response = await _anonymousClient.GetAsync(Endpoint);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<GetCurriculumVitaeResponse>() 
                   ?? new GetCurriculumVitaeResponse();
        }

        return new GetCurriculumVitaeResponse();
    }
    
    public async Task<Result<PatchCurriculumVitaeResponse>> PatchCurriculumVitaeAsync(PatchCurriculumVitaeRequest request)
    {
        var response = await _authorizedclient.PatchAsJsonAsync($"{Endpoint}", request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<PatchCurriculumVitaeResponse>() ?? new();   
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>() ?? ErrorResponse.CreateDefault();
        return new Result<PatchCurriculumVitaeResponse>(new ApiResponseException(errorResponse.Message));
    }
    
}