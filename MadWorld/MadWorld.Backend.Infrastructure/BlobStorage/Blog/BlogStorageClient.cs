using LanguageExt;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Storage;

namespace MadWorld.Backend.Infrastructure.BlobStorage.Blog;

public class BlogStorageClient : IBlogStorageClient
{
    private const string BlogPagePath = "Blog/Pages";
    
    private readonly IStorageClient _client;

    public BlogStorageClient(IStorageClient client)
    {
        _client = client;
    }
    
    public Option<string> GetPageAsBase64(string id)
    {
        var name = $"{id}.html";
        
        return _client.GetBase64Body(name, BlogPagePath);
    }
}