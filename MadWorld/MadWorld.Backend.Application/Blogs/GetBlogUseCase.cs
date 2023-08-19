using System.ComponentModel.DataAnnotations;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Backend.Application.Blogs;

public class GetBlogUseCase : IGetBlogUseCase
{
    private readonly IBlogRepository _repository;

    public GetBlogUseCase(IBlogRepository repository)
    {
        _repository = repository;
    }
    
    public Result<GetBlogResponse> GetBlog(GetBlogRequest request)
    {
        var id = GuidId.Parse(request.Id);

        if (id.IsFaulted)
        {
            return new Result<GetBlogResponse>(new ValidationException($"{nameof(request.Id)} must be a valid guid"));
        }
        
        var blog = _repository.GetBlog(id.GetValue());

        return blog.Match(
            ToResponse,
            () => new Result<GetBlogResponse>(new ValidationException($"Blog with id {id} not found"))
        );
    }
    
    private static Result<GetBlogResponse> ToResponse(Blog blog)
    {
        var contract = blog.ToDetailContract();
        
        return new GetBlogResponse()
        {
            Blog = contract
        };
    }
}