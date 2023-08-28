using MadWorld.Backend.Domain.Blogs;

namespace MadWorld.Backend.Application.Blogs;

public class BlogDeletedCleanupUseCase : IBlogDeletedCleanupUseCase
{
    private readonly IBlogRepository _repository;
    private readonly IBlogStorageClient _storageClient;

    public BlogDeletedCleanupUseCase(IBlogRepository repository, IBlogStorageClient storageClient)
    {
        _repository = repository;
        _storageClient = storageClient;
    }
    
    public async Task StartCleanUp()
    {
        var blogs = _repository.GetDeletedBlogs();

        foreach (var blog in blogs.Where(b => b.IsThirtyDaysOld()))
        {
            await _storageClient.DeletePageAsync(blog.Id);
            _repository.DeleteBlog(blog);
        }
    }
}