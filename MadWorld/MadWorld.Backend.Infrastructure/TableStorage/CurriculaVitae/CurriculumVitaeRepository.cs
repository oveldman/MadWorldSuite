using static LanguageExt.Prelude;

using Azure.Data.Tables;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.Properties;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;

public class CurriculumVitaeRepository : ICurriculumVitaeRepository
{
    public const string TableName = "CurriculaVitae";
    
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
                => c.PartitionKey == CurriculumVitaeEntity.PartitionKeyName &&
                   c.RowKey == CurriculumVitaeEntity.RowKeyName)
            .FirstOrDefault());

        return curriculumVitae.Match(
            ToCurriculumVitae,
            () => Option<CurriculumVitae>.None);
    }
    
    public Result<bool> UpdateCurriculumVitae(CurriculumVitae curriculumVitae)
    {
        var entity = ToCurriculumVitaeEntity(curriculumVitae);

        var response = _table.UpsertEntity(entity);

        if (!response.IsError) return true;
        
        var body = response.Content.ToString();
        var exception = new TableStorageException(body);
        _logger.LogError(exception, "The table storage has an error");
        return new Result<bool>(exception);
    }

    private static Option<CurriculumVitae> ToCurriculumVitae(CurriculumVitaeEntity entity)
    {
        var fullName = (Text)entity.FullName;
        var birthDate = (BirthDate)entity.BirthDate;
        
        return new CurriculumVitae(fullName, birthDate);
    }
    
    private static CurriculumVitaeEntity ToCurriculumVitaeEntity(CurriculumVitae curriculumVitae)
    {
        return new CurriculumVitaeEntity
        {
            FullName = curriculumVitae.FullName,
            BirthDate = curriculumVitae.BirthDate
        };
    }
}