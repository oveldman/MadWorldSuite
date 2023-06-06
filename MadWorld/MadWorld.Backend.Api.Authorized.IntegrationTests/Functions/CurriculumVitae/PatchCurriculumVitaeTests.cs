using Azure.Data.Tables;
using MadWorld.Backend.API.Authorized.Functions.CurriculumVitae;
using MadWorld.Backend.Domain.CurriculaVitae;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Infrastructure.TableStorage.CurriculaVitae;
using MadWorld.IntegrationTests.Extensions;
using MadWorld.Shared.Contracts.Authorized.CurriculumVitae;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.CurriculumVitae;

[Collection("AzuriteDocker")]
public class PatchCurriculumVitaeTests : IClassFixture<ApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly ApiStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    private TableClient _tableClient = null!;
    
    private readonly PatchCurriculumVitae _function;

    public PatchCurriculumVitaeTests(ApiDockerStartupFactory factory)
    {
        _factory = factory;

        var useCase = factory.Host.Services.GetRequiredService<IPatchCurriculumVitaeUseCase>();
        _function = new PatchCurriculumVitae(useCase);
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
    }

    [Fact]
    public async Task PatchCurriculum_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var request = new PatchCurriculumVitaeRequest()
        {
            FullName = "Katsuo Mariko"
        };

        var context = new Mock<FunctionContext>();
        var httpRequest = new Mock<HttpRequestData>(context.Object);
        
        context.Setup(c => c.InstanceServices)
            .Returns(_factory.Host.Services);
        
        httpRequest.Setup(hr => hr.Body).Returns(request.ToMemoryStream());
        
        // Act
        var response = await _function.Run(httpRequest.Object, context.Object);
        
        // Assert
        var contract = response.GetValue();
        contract.Succeeded.ShouldBeTrue();

        var cv = _tableClient.Query<CurriculumVitaeEntity>().FirstOrDefault();
        cv!.FullName.ShouldBe("Katsuo Mariko");
    }

    public Task InitializeAsync()
    {
        _tableServiceClient.CreateTableIfNotExists(CurriculumVitaeRepository.TableName);
        _tableClient = _tableServiceClient.GetTableClient(CurriculumVitaeRepository.TableName);
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}