using Azure.Data.Tables;
using MadWorld.Backend.API.Anonymous.Functions.CurriculumVitae;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace MadWorld.Backend.Api.Anonymous.IntegrationTests.Functions.CurriculumVitae;

[Collection(CollectionTypes.IntegrationTests)]
public class GetCurriculumVitaeTests : IClassFixture<AnonymousApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly AnonymousApiStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    
    private readonly GetCurriculumVitae _function;

    public GetCurriculumVitaeTests(AnonymousApiDockerStartupFactory factory)
    {
        _factory = factory;

        var useCase = factory.Host.Services.GetRequiredService<IGetCurriculumVitaeUseCase>();
        _function = new GetCurriculumVitae(useCase);
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
    }

    [Fact]
    public void GetCurriculumVitae_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var context = new Mock<FunctionContext>();
        var request = new Mock<HttpRequestData>(context.Object);
        
        // Act
        var response = _function.Run(request.Object, context.Object);
        
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