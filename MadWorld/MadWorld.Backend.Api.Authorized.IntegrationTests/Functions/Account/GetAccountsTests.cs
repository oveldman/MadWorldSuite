using LanguageExt.Pretty;
using MadWorld.Backend.API.Authorized.Functions.Account;
using MadWorld.Backend.Domain.Accounts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using WireMock.Admin.Mappings;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.Account;

public sealed class GetAccountsTests : IClassFixture<ApiStartupFactory>, IAsyncLifetime
{
    private readonly ApiStartupFactory _factory;
    private readonly WireMockServer _wireMockServer;
    private readonly GetAccounts _function;

    public GetAccountsTests(ApiStartupFactory factory)
    {
        _factory = factory;
        _wireMockServer = factory.Host.Services.GetRequiredService<WireMockServer>();
        
        var useCase = factory.Host.Services.GetRequiredService<IGetAccountsUseCase>();
        _function = new GetAccounts(useCase);
    }

    [Fact]
    public async Task GetAccounts_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var context = new Mock<FunctionContext>();
        var request = new Mock<HttpRequestData>(context.Object);

        _wireMockServer.Given(
            Request.Create().WithPath("/users").UsingGet()
        ).RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type",
                    "application/json;odata.metadata=minimal;odata.streaming=true;IEEE754Compatible=false;charset=utf-8")
                .WithBody(GetGraphExplorerResponse())
        );

        // Act
        var response = await _function.Run(request.Object, context.Object);

        // Assert
        response.Accounts.Count.ShouldBe(2);
        response.Accounts.ElementAt(0).Id = "e88faade-0c94-40c7-868d-5fd9d2982089";
        response.Accounts.ElementAt(0).Name = "Gerald Ruben";
        response.Accounts.ElementAt(0).IsResourceOwner = false;
        response.Accounts.ElementAt(1).IsResourceOwner = true;
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }

    private static string GetGraphExplorerResponse()
    {
        return """
        {
            "@odata.context": "https://graph.microsoft.com/v1.0/$metadata#users",
            "value": [
                {
                    "id": "e88faade-0c94-40c7-868d-5fd9d2982089",
                    "displayName": "Gerald Ruben",
                    "mailNickname": "e88faade-0c94-40c7-868d-5fd9d2982089"
                },
                {
                    "id": "cf38d987-8fbb-49b9-bc34-e086bf69374d",
                    "displayName": "Jennifer Luuk",
                    "mailNickname": "test@test.nl#EXT#"
                }
            ]
        }
        """;
    }
}