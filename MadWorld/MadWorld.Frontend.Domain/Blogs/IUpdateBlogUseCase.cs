using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Frontend.Domain.Blogs;

public interface IUpdateBlogUseCase
{
    Task<OkResponse> UpdateBlog(BlogDetailContract contract, string bodyUtf8);
}