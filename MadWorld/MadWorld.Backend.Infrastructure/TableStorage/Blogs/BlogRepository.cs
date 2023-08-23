using static LanguageExt.Prelude;
using Azure.Data.Tables;
using LanguageExt;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Infrastructure.TableStorage.Extensions;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Infrastructure.TableStorage.Blogs;

public class BlogRepository : IBlogRepository
{
    public const string TableName = "Blog";
    
    private readonly TableClient _table;
    
    public BlogRepository(TableServiceClient client)
    {
        client.CreateTableIfNotExists(TableName);
        _table = client.GetTableClient(TableName);
    }
    
    public IReadOnlyList<Blog> GetBlogs(int page)
    {
        var blogs = _table
            .Query<BlogEntity>(c
                => c.PartitionKey == BlogEntity.PartitionKeyName && !c.IsDeleted)
            .AsPages(pageSizeHint: TableStorageConfigurationsManager.DefaultPageSize)
            .Skip(page)
            .FirstOrDefault()?
            .Values
            .Select(ToBlog)
            .ToList() ?? new List<Blog>();

        return blogs;
    }
    
    public Option<Blog> GetBlog(GuidId id)
    {
        var blog = Optional(_table
            .Query<BlogEntity>(c
                => c.PartitionKey == BlogEntity.PartitionKeyName &&
                   c.Identifier == id &&
                   !c.IsDeleted)
            .FirstOrDefault());

        return blog.Match(b => 
                ToBlog(b), 
                () => Option<Blog>.None);
    }
    
    private static Blog ToBlog(BlogEntity entity)
    {
        var id = (GuidId)entity.Identifier;
        var title = (Text)entity.Title;
        var writer = (Text)entity.Writer;
        
        return new Blog(id, title, writer, entity.Created, entity.Updated, entity.IsDeleted);
    }
    
    private static BlogEntity ToBlobEntity(Blog blog)
    {
        return new BlogEntity
        {
            RowKey = blog.Created.ToRowKeyDesc(),
            Identifier = blog.Id,
            Title = blog.Title,
            Writer = blog.Writer,
            Created = blog.Created,
            Updated = blog.Updated
        };
    }
}