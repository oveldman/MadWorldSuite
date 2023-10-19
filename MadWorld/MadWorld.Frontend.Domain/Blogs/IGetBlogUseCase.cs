using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Frontend.Domain.Blogs;

public interface IGetBlogUseCase
{
    Task<BlogDetailContract> GetBlog(string id);
}