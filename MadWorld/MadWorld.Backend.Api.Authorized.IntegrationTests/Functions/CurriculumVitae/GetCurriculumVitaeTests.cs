using Azure.Data.Tables;
using MadWorld.Backend.API.Authorized.Functions.CurriculumVitae;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.CurriculumVitae;

[Collection(CollectionTypes.IntegrationTests)]
public class GetCurriculumVitaeTests : IClassFixture<AuthorizedApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly AuthorizedApiStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    
    private readonly GetCurriculumVitae _function;

    public GetCurriculumVitaeTests(AuthorizedApiDockerStartupFactory factory)
    {
        _factory = factory;
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();

        var useCase = factory.Host.Services.GetRequiredService<IGetCurriculumVitaeUseCase>();
        _function = new GetCurriculumVitae(useCase);
    }

    [Fact]
    public void GetCurriculumVitae_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var context = Substitute.For<FunctionContext>();
        var request = Substitute.For<HttpRequestData>(context);
        
        // Act
        var response = _function.Run(request, context);
        
        // Assert
        var contract = response
            .Match(r => r, () => default!);
        
        contract.CurriculumVitae.FullName.ShouldBe("Hedwig Constanze");
    }

    public Task InitializeAsync()
    {
        _tableServiceClient.CreateTableIfNotExists(CurriculumVitaeRepository.TableName);
        var client = _tableServiceClient.GetTableClient(CurriculumVitaeRepository.TableName);
        client.AddEntity(new CurriculumVitaeEntity()
        {
            FullName = "Hedwig Constanze",
            BirthDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        });
        
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}