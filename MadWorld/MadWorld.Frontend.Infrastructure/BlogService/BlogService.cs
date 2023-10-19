using System.Net.Http.Json;
using MadWorld.Frontend.Domain.Api;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;

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
    public async Task<GetBlogsResponse> GetBlogs(int pageNumber)
    {
        return await _client.GetFromJsonAsync<GetBlogsResponse>($"{EndpointPlural}/{pageNumber}") ?? new GetBlogsResponse(0, Array.Empty<BlogContract>());
    }
    
    public async Task<GetBlogResponse> GetBlog(string id)
    {
        return await _client.GetFromJsonAsync<GetBlogResponse>($"{EndpointSingular}/{id}") ?? new GetBlogResponse();
    }
}