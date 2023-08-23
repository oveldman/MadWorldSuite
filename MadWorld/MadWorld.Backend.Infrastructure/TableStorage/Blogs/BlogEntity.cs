using Azure;
using Azure.Data.Tables;

namespace MadWorld.Backend.Infrastructure.TableStorage.Blogs;

public class BlogEntity : ITableEntity
{
    public const string PartitionKeyName = "Blog";
    public string PartitionKey { get; set; } = PartitionKeyName;
    public string RowKey { get; set; } = string.Empty;
    
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    
    public string Identifier { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Writer { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.MinValue;
    public DateTime Updated { get; set; } = DateTime.MinValue;
    public bool IsDeleted { get; set; }
}