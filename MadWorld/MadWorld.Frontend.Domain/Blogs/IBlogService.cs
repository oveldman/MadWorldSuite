using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Frontend.Domain.Blogs;

public interface IBlogService
{
    Task<GetBlogsResponse> GetBlogs(int pageNumber);
}