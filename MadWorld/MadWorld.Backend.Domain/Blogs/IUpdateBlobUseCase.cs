using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Backend.Domain.Blogs;

public interface IUpdateBlobUseCase
{
    OkResponse UpdateBlob(UpdateBlogRequest request);
}