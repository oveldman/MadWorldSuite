using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Backend.Application.Blogs;

public class UpdateBlobUseCase : IUpdateBlobUseCase
{
    private readonly IBlogRepository _repository;
    private readonly IBlogStorageClient _storageClient;

    public UpdateBlobUseCase(IBlogRepository repository, IBlogStorageClient storageClient)
    {
        _repository = repository;
        _storageClient = storageClient;
    }
    
    public Result<Option<OkResponse>> UpdateBlob(UpdateBlogRequest? request)
    {
        if (request == null) return new Result<Option<OkResponse>>(new ValidationException("Request cannot be null"));
        if (request.Blog == null) return new Result<Option<OkResponse>>(new ValidationException("Blog cannot be null"));

        var blog = Blog.Parse(request.Id, request.Blog.Title, request.Blog.Writer);
        
        return blog.Match(
            b => UpdateBlog(b, request.Blog.Body).GetAwaiter().GetResult(),
            e => new Result<Option<OkResponse>>(e));
    }

    private async Task<Result<Option<OkResponse>>> UpdateBlog(Blog blog, string body)
    {
        var currentBlog = _repository.GetBlog(blog.Id);
        if (currentBlog.IsNone) return new Option<OkResponse>();

        var currentBlogValue = currentBlog.ValueUnsafe();
        currentBlogValue.Update(blog.Title, blog.Writer);
        
        _repository.UpsertBlog(currentBlogValue);
        await _storageClient.UpsertPageAsBase64Async(blog.Id, body);
        
        return new Result<Option<OkResponse>>(new OkResponse());
    }
}