using MadWorld.Backend.Domain.Blogs;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Application.Blogs;

public class BlogDeletedCleanupUseCase : IBlogDeletedCleanupUseCase
{
    private readonly IBlogRepository _repository;
    private readonly IBlogStorageClient _storageClient;
    private readonly ILogger<BlogDeletedCleanupUseCase> _logger;

    public BlogDeletedCleanupUseCase(
        IBlogRepository repository, 
        IBlogStorageClient storageClient, 
        ILogger<BlogDeletedCleanupUseCase> logger)
    {
        _repository = repository;
        _storageClient = storageClient;
        _logger = logger;
    }
    
    public async Task StartCleanUp()
    {
        var blogs = _repository.GetDeletedBlogs();
        
        foreach (var blog in blogs.Where(b => b.IsThirtyDaysOld()))
        {
            await PermanentDeleteBlog(blog);
        }
    }
    
    private async Task PermanentDeleteBlog(Blog blog)
    {
        _logger.LogInformation("Start permanent delete blog {BlogId} with title {Title}",
            blog.Id.ToString(),
            blog.Title.ToString());
        
        await _storageClient.DeletePageAsync(blog.Id);
        _repository.DeleteBlog(blog);
        
        _logger.LogInformation("Successfully blog deleted {BlogId} with title {Title}",
            blog.Id.ToString(),
            blog.Title.ToString());
    }
}