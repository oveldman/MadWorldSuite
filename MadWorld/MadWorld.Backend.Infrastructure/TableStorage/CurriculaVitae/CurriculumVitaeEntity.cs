using Azure;
using Azure.Data.Tables;
using MadWorld.Backend.Domain.CurriculaVitae;

namespace MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;

public class CurriculumVitaeEntity : ITableEntity
{
    public const string PartitionKeyName = "CurriculumVitae";
    
    public string PartitionKey { get; set; } = PartitionKeyName;
    public required string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    
    public string FullName { get; set; } = string.Empty;
}