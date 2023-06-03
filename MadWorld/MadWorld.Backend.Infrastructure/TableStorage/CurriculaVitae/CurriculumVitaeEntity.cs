using Azure;
using Azure.Data.Tables;

namespace MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;

public sealed class CurriculumVitaeEntity : ITableEntity
{
    public const string PartitionKeyName = "CurriculumVitae";
    public const string RowKeyName = "3972a8a0-8c32-46ee-bb68-4d03b956e1cd";
    
    public string PartitionKey { get; set; } = PartitionKeyName;
    public string RowKey { get; set; } = RowKeyName;
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }
    
    public string FullName { get; set; } = string.Empty;
}