using System.Text;
using MadWorld.Frontend.Application.Blogs.Mapper;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Frontend.Application.Blogs;

public class UpdateBlogUseCase : IUpdateBlogUseCase
{
    private readonly IBlogService _service;

    public UpdateBlogUseCase(IBlogService service)
    {
        _service = service;
    }
    
    public async Task<OkResponse> UpdateBlog(BlogDetailContract contract, string bodyUtf8)
    {
        var bodyBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(bodyUtf8));
        
        var request = BlogMapper.ToUpdateContract(contract, bodyBase64);
        var response = await _service.UpdateBlog(request);
        return response.Match(
            okResponse => okResponse,
            _ => new OkResponse()
        );
    }
}