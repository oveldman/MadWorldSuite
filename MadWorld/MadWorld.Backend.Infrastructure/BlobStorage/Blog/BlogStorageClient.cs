using LanguageExt;
using MadWorld.Backend.Domain.Blogs;

namespace MadWorld.Backend.Infrastructure.BlobStorage.Blog;

public class BlogStorageClient : IBlogStorageClient
{
    private const string BlogPagePath = "Blog/Pages";
    
    private readonly BlobStorageClient _client;

    public BlogStorageClient(BlobStorageClient client)
    {
        _client = client;
    }
    
    public Option<string> GetPageAsBase64(string id)
    {
        var name = $"{id}.html";
        
        return _client.GetBase64Body(name, BlogPagePath);
    }
}