using MadWorld.Backend.API.Authorized.Functions.Account;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.LanguageExt;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.Account;

public sealed class GetAccountTests : IClassFixture<ApiStartupFactory>, IAsyncLifetime
{
    private readonly ApiStartupFactory _factory;
    private readonly WireMockServer _wireMockServer;
    private readonly GetAccount _function;

    public GetAccountTests(ApiStartupFactory factory)
    {
        _factory = factory;
        _wireMockServer = factory.Host.Services.GetRequiredService<WireMockServer>();
        
        var useCase = factory.Host.Services.GetRequiredService<IGetAccountUseCase>();
        _function = new GetAccount(useCase);
    }
    
    [Fact]
    public void GetAccount_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        const string userId = "4ce4ed46-cd2e-4529-88c0-57ed36e0f91b";
        
        var context = new Mock<FunctionContext>();
        var request = new Mock<HttpRequestData>(context.Object);

        _wireMockServer.Given(
            Request.Create().WithPath($"/users/{userId}").UsingGet()
        ).RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json;odata.metadata=minimal;odata.streaming=true;IEEE754Compatible=false;charset=utf-8")
                .WithBody(GetGraphExplorerResponse())
        );

        // Act
        var response = _function.Run(request.Object, context.Object, userId);
        
        // Assert
        var result = response
            .GetValue()
            .Match(r => r, () => default!);
        
        result.Account.Id.ShouldBe("e88faade-0c94-40c7-868d-5fd9d2982089");
        result.Account.Name.ShouldBe("Gerald Ruben");
        result.Account.IsResourceOwner.ShouldBe(false);
        result.Account.Roles.Count.ShouldBe(3);
        result.Account.Roles.ShouldContain("Admin");
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
            "@odata.context": "https://graph.microsoft.com/v1.0/$metadata#users(id,displayName,mailNickname,extension_ApplicationId_Roles)/$entity",
            "id": "e88faade-0c94-40c7-868d-5fd9d2982089",
            "displayName": "Gerald Ruben",
            "mailNickname": "e88faade-0c94-40c7-868d-5fd9d2982089",
            "extension_ApplicationId_Roles": "None;User;Admin"
        }
        """;
    }
}