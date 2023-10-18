using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Frontend.Application.Blogs;

public class GetBlogsUseCase : IGetBlogsUseCase
{
    private readonly IBlogService _service;
    public GetBlogsUseCase(IBlogService service)
    {
        _service = service;
    }
    
    public async Task<GetBlogsResponse> GetBlogsAsync(int pageNumber)
    {
        return await _service.GetBlogs(pageNumber);
    }
}