using System.Net.Http.Json;
using LanguageExt.Common;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Error;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Frontend.Infrastructure.BlogService;

public class BlogService : IBlogService
{
    private const string EndpointPlural = "Blogs";
    private const string EndpointSingular = "Blog";
    
    private readonly HttpClient _client;
    
    public BlogService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.MadWorldApiAuthorized);
    }
    
    public async Task<Result<OkResponse>> AddBlog(AddBlogRequest request)
    {
        var response = await _client.PostAsJsonAsync(EndpointSingular, request);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<OkResponse>() ?? new();   
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>() ?? ErrorResponse.CreateDefault();
        return new Result<OkResponse>(new ApiResponseException(errorResponse.Message));
    }
    
    public async Task<OkResponse> DeleteBlog(string id)
    {
        return await _client.DeleteFromJsonAsync<OkResponse>($"{EndpointSingular}/{id}") ?? new OkResponse();
    }
    
    public async Task<GetBlogsResponse> GetBlogs(int pageNumber)
    {
        return await _client.GetFromJsonAsync<GetBlogsResponse>($"{EndpointPlural}/{pageNumber}") ?? new GetBlogsResponse(0, Array.Empty<BlogContract>());
    }
    
    public async Task<GetBlogResponse> GetBlog(string id)
    {
        return await _client.GetFromJsonAsync<GetBlogResponse>($"{EndpointSingular}/{id}") ?? new GetBlogResponse();
    }
    
    public async Task<Result<OkResponse>> UpdateBlog(UpdateBlogRequest request)
    {
        var response = await _client.PatchAsJsonAsync(EndpointSingular, request);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<OkResponse>() ?? new();   
        }

        var errorResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>() ?? ErrorResponse.CreateDefault();
        return new Result<OkResponse>(new ApiResponseException(errorResponse.Message));
    }
}