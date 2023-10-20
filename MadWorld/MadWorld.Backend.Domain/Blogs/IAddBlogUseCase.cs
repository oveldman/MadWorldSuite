using LanguageExt.Common;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Backend.Domain.Blogs;

public interface IAddBlogUseCase
{
    Result<OkResponse> AddBlob(AddBlogRequest? request, string username);
}