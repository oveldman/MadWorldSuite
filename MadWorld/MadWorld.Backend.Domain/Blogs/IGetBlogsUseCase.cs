using LanguageExt.Common;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Backend.Domain.Blogs;

public interface IGetBlogsUseCase
{
    Task<Result<GetBlogsResponse>> GetBlogsAsync(GetBlogsRequest request);
}