using LanguageExt.Common;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Backend.Domain.Blogs;

public interface IGetBlogUseCase
{
    Result<GetBlogResponse> GetBlog(GetBlogRequest request);
}