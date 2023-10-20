using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Frontend.Domain.Blogs;

public interface IAddBlogUseCase
{
    Task<OkResponse> AddBlog(BlogDetailContract contract, string bodyUtf8);
}