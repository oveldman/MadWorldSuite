using Azure;
using static LanguageExt.Prelude;
using Azure.Data.Tables;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Infrastructure.TableStorage.Extensions;

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

    public async Task<int> CountBlogs()
    {
            var results = _table.QueryAsync<BlogEntity>(b
                    => b.PartitionKey == BlogEntity.PartitionKeyName && !b.IsDeleted,
                select: new[] { "PartitionKey" });
            return await results.CountAsync();
    }

    public Result<Unit> DeleteBlog(Blog blog)
    {
        var entity = ToBlobEntity(blog);
        _table.DeleteEntity(entity.PartitionKey, entity.RowKey);
        return Unit.Default;
    }
    
    public IReadOnlyList<Blog> GetBlogs(int page)
    {
        var blogs = _table
            .Query<BlogEntity>(b
                => b.PartitionKey == BlogEntity.PartitionKeyName && !b.IsDeleted)
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
    
    public IReadOnlyList<Blog> GetDeletedBlogs()
    {
        return _table
            .Query<BlogEntity>(c
                => c.PartitionKey == BlogEntity.PartitionKeyName && c.IsDeleted)
            .Select(ToBlog)
            .ToList();
    }
    
    public Result<Unit> UpsertBlog(Blog blog)
    {
        var entity = ToBlobEntity(blog);
        _table.UpsertEntity(entity);
        return Unit.Default;
    }
    
    private static Blog ToBlog(BlogEntity entity)
    {
        var id = (GuidId)entity.Identifier;
        var title = (Text)entity.Title;
        var writer = (Text)entity.Writer;
        
        var created = (DateTimeUtc)entity.Created;
        var updated = (DateTimeUtc)entity.Updated;
        
        return new Blog(id, title, writer, created, updated, entity.IsDeleted);
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
            Updated = blog.Updated,
            IsDeleted = blog.IsDeleted
        };
    }
}