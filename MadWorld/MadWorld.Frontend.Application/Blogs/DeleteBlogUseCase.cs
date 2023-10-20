using MadWorld.Frontend.Domain.Blogs;

namespace MadWorld.Frontend.Application.Blogs;

public class DeleteBlogUseCase : IDeleteBlogUseCase
{
    private readonly IBlogService _service;

    public DeleteBlogUseCase(IBlogService service)
    {
        _service = service;
    }

    public async Task<bool> DeleteBlog(string id)
    {
        var response = await _service.DeleteBlog(id);
        return response.IsSuccess;
    }
}