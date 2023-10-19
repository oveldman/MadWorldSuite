using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Frontend.Application.Blogs;

public class GetBlogUseCase : IGetBlogUseCase
{
    private readonly IBlogService _service;

    public GetBlogUseCase(IBlogService service)
    {
        _service = service;
    }
    
    public async Task<BlogDetailContract> GetBlog(string id)
    {
        var response = await _service.GetBlog(id);
        return response.Blog;
    }
}