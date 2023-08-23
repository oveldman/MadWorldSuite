using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Backend.Application.Blogs;

public class DeleteBlogUseCase : IDeleteBlogUseCase
{
    private readonly IBlogRepository _repository;

    public DeleteBlogUseCase(IBlogRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Option<OkResponse>> DeleteBlog(DeleteBlogRequest request)
    {
        throw new NotImplementedException();
    }
}