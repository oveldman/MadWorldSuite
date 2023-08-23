using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Shared.Contracts.Anonymous.Blog;
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
        var id = GuidId.Parse(request.Id);

        if (id.IsFaulted)
        {
            return new Result<Option<OkResponse>>(new ValidationException($"{nameof(request.Id)} must be a valid guid"));
        }
        
        var blog = _repository.GetBlog(id.GetValue());
        return blog.Match(
            DeleteBlog,
            () => Option<OkResponse>.None);
    }

    private Result<Option<OkResponse>> DeleteBlog(Blog blog)
    {
        blog.SoftDelete();

        _repository.UpsertBlog(blog);

        return Option<OkResponse>.Some(
            new OkResponse()
            {
                Message = $"Blog with id '{blog.Id}' has been deleted"
            });
    }
}