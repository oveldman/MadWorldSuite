using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Storage;

namespace MadWorld.Backend.Infrastructure.BlobStorage.Blog;

public class BlogStorageClient : IBlogStorageClient
{
    private const string BlogPagePath = "Blog/Pages";
    
    private static string BlogPageName(string id) => $"{id}.html";
    
    private readonly IStorageClient _client;

    public BlogStorageClient(IStorageClient client)
    {
        _client = client;
    }

    public Task<Result<Unit>> DeletePageAsync(string id)
    {
        var name = BlogPageName(id);

        return _client.DeleteAsync(name, BlogPagePath);
    }

    public Option<string> GetPageAsBase64(string id)
    {
        var name = BlogPageName(id);
        
        return _client.GetBase64Body(name, BlogPagePath);
    }
    
    public async Task<Result<Unit>> UpsertPageAsBase64Async(string id, string body)
    {
        var name = BlogPageName(id);
        
        return await _client.UpsertBase64Body(name, BlogPagePath, body);
    }
}