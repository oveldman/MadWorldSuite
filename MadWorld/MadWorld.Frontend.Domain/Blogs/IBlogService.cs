using LanguageExt.Common;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Frontend.Domain.Blogs;

public interface IBlogService
{
    Task<Result<OkResponse>> AddBlog(AddBlogRequest request);
    Task<OkResponse> DeleteBlog(string id);
    Task<GetBlogsResponse> GetBlogs(int pageNumber);
    Task<GetBlogResponse> GetBlog(string id);
    Task<Result<OkResponse>> UpdateBlog(UpdateBlogRequest request);
}