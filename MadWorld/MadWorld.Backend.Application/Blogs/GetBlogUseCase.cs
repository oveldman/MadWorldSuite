using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Backend.Application.Blogs;

public class GetBlogUseCase : IGetBlogUseCase
{
    private readonly IBlogRepository _repository;
    private readonly IBlogStorageClient _storageClient;

    public GetBlogUseCase(IBlogRepository repository, IBlogStorageClient storageClient)
    {
        _repository = repository;
        _storageClient = storageClient;
    }
    
    public Result<Option<GetBlogResponse>> GetBlog(GetBlogRequest request)
    {
        var id = GuidId.Parse(request.Id);

        if (id.IsFaulted)
        {
            return new Result<Option<GetBlogResponse>>(new ValidationException($"{nameof(request.Id)} must be a valid guid"));
        }
        
        var blog = _repository.GetBlog(id.GetValue());

        return blog.Match(
            ToResponse,
            () => Option<GetBlogResponse>.None
        );
    }
    
    private Result<Option<GetBlogResponse>> ToResponse(Blog blog)
    {
        var body = _storageClient.GetPageAsBase64(blog.Id);
        
        var contract = blog.ToDetailContract();
        
        contract.Body = body.Match(
            b => b, 
            () => string.Empty);

        return Option<GetBlogResponse>.Some(
            new GetBlogResponse()
            {
                Blog = contract
            });
    }
}