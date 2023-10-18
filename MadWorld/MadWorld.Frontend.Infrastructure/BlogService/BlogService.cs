using System.Net.Http.Json;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Frontend.Infrastructure.BlogService;

public class BlogService : IBlogService
{
    private const string Endpoint = "Blogs";
    
    private readonly HttpClient _client;
    
    public BlogService(IHttpClientFactory clientFactory)
    {
        _client = clientFactory.CreateClient(ApiTypes.MadWorldApiAuthorized);
    }
    public async Task<GetBlogsResponse> GetBlogs(int pageNumber)
    {
        return await _client.GetFromJsonAsync<GetBlogsResponse>($"{Endpoint}/{pageNumber}") ?? new GetBlogsResponse(0, Array.Empty<BlogContract>());
    }
}