using System.Reflection.Metadata;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Backend.Application.Blogs;

public class AddBlogUseCase : IAddBlogUseCase
{
    private readonly IBlogRepository _repository;
    private readonly IBlogStorageClient _storageClient;

    public AddBlogUseCase(IBlogRepository repository, IBlogStorageClient storageClient)
    {
        _repository = repository;
        _storageClient = storageClient;
    }

    public Result<OkResponse> AddBlob(AddBlogRequest? request, string username)
    {
        if (request == null) return new Result<OkResponse>(new ValidationException("Request cannot be null"));
        if (request.Blog == null) return new Result<OkResponse>(new ValidationException("Blog cannot be null"));
        
        var blog = Blog.Parse(
            request.Blog.Title,
            username);

        return blog.Match(
            b => AddBlob(b, request.Blog.Body).GetAwaiter().GetResult(),
            e => new Result<OkResponse>(e));
    }

    private async Task<Result<OkResponse>> AddBlob(Blog blog, string body)
    {
        _repository.UpsertBlog(blog);
        await _storageClient.UpsertPageAsBase64Async(blog.Id, body);

        return new Result<OkResponse>(new OkResponse());
    }
}