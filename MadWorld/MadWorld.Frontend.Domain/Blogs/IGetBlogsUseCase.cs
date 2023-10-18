using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Frontend.Domain.Blogs;

public interface IGetBlogsUseCase
{
    Task<GetBlogsResponse> GetBlogsAsync(int pageNumber);
}