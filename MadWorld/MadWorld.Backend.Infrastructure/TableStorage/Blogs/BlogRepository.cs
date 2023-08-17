using Azure.Data.Tables;
using LanguageExt;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Infrastructure.TableStorage.Blogs;

public class BlogRepository : IBlogRepository
{
    public const string TableName = "Blog";
    
    private readonly ILogger<BlogRepository> _logger;
    private readonly TableClient _table;
    
    public BlogRepository(TableServiceClient client, ILogger<BlogRepository> logger)
    {
        _logger = logger;
        client.CreateTableIfNotExists(TableName);
        _table = client.GetTableClient(TableName);
    }
    
    private static Option<Blog> ToBlog(BlogEntity entity)
    {
        var id = (GuidId)entity.RowKey;
        var title = (Text)entity.Title;
        var writer = (Text)entity.Writer;
        
        return new Blog(id, title, writer, entity.Created, entity.Updated);
    }
    
    private static BlogEntity ToBlobEntity(Blog blog)
    {
        return new BlogEntity
        {
            RowKey = blog.Id.ToString(),
            Title = blog.Title,
            Writer = blog.Writer,
            Created = blog.Created,
            Updated = blog.Updated
        };
    }
}