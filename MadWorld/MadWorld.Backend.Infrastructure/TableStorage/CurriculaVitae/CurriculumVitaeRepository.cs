using static LanguageExt.Prelude;

using Azure.Data.Tables;
using LanguageExt;
using MadWorld.Backend.Domain.CurriculaVitae;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;

public class CurriculumVitaeRepository : ICurriculumVitaeRepository
{
    private const string TableName = "CurriculaVitae";
    
    private readonly ILogger<CurriculumVitaeRepository> _logger;
    private readonly TableClient _table;
    
    public CurriculumVitaeRepository(TableServiceClient client, ILogger<CurriculumVitaeRepository> logger)
    {
        _logger = logger;
        client.CreateTableIfNotExists(TableName);
        _table = client.GetTableClient(TableName);
    }
    
    public Option<CurriculumVitae> GetCurriculumVitae()
    {
        var curriculumVitae = Optional(_table
            .Query<CurriculumVitaeEntity>(c 
                => c.PartitionKey == CurriculumVitaeEntity.PartitionKeyName)
            .FirstOrDefault());

        return curriculumVitae.Match(
            cve =>
            {
                return CurriculumVitae.Parse(cve.FullName).Match(
                    cv => cv,
                    exception =>
                    {
                        _logger.LogError(exception, "The curriculum vitae entity has invalid data in the table storage");
                        return Option<CurriculumVitae>.None;
                    });
            },
            () => Option<CurriculumVitae>.None);
    }
}