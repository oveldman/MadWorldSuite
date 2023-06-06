using MadWorld.Backend.API.Authorized.Functions.Account;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.IntegrationTests.Extensions;
using MadWorld.Shared.Contracts.Authorized.Account;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using WireMock.FluentAssertions;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.Account;

[Collection(CollectionTypes.IntegrationTests)]
public sealed class PatchAccountTests : IClassFixture<AuthorizedApiStartupFactory>, IAsyncLifetime
{
    private readonly AuthorizedApiStartupFactory _factory;
    private readonly WireMockServer _wireMockServer;
    private readonly PatchAccount _function;

    public PatchAccountTests(AuthorizedApiStartupFactory factory)
    {
        _factory = factory;
        _wireMockServer = factory.Host.Services.GetRequiredService<WireMockServer>();
        
        var useCase = factory.Host.Services.GetRequiredService<IPatchAccountUseCase>();
        _function = new PatchAccount(useCase);
    }

    [Fact]
    public async Task PatchAccount_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var request = new PatchAccountRequest()
        {
            Id = "4ce4ed46-cd2e-4529-88c0-57ed36e0f91b",
            Roles = new[]
            {
                "None",
                "User",
                "Admin"
            }
        };

        var context = new Mock<FunctionContext>();
        var httpRequest = new Mock<HttpRequestData>(context.Object);

        context.Setup(c => c.InstanceServices)
            .Returns(_factory.Host.Services);

        httpRequest.Setup(hr => hr.Body).Returns(request.ToMemoryStream());

        _wireMockServer.Given(
            Request.Create()
                .WithPath($"/users/{request.Id}")
                .UsingPatch()
                .WithBody(GetGraphExplorerRequest())
        ).RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type",
                    "application/json;odata.metadata=minimal;odata.streaming=true;IEEE754Compatible=false;charset=utf-8")
                .WithBody("{}")
        );

        // Act
        var response = await _function.Run(httpRequest.Object, context.Object);

        // Assert
        response.IsSuccess.ShouldBeTrue();
        _wireMockServer
            .Should()
            .HaveReceived(1)
            .Calls()
            .UsingPatch();
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
    
    private static string GetGraphExplorerRequest()
    {
        return "{\"@odata.type\":\"#microsoft.graph.user\",\"extension_ApplicationId_Roles\":\"None;User;Admin\"}";
    }
}