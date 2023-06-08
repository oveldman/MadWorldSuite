using System.Text.Json;
using Bunit;
using Bunit.TestDoubles;
using MadWorld.Frontend.UI.Admin.Pages.Accounts;
using MadWorld.Frontend.UI.Shared.Dependencies;
using MadWorld.IntegrationTests.BUnit;
using MadWorld.IntegrationTests.Startups;
using MadWorld.Shared.Contracts.Authorized.Account;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Radzen.Blazor;
using Shouldly;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;

namespace MadWorld.Frontend.UI.Admin.Integrations.Pages.Accounts;

public class AccountDetailsTests : IClassFixture<UiStartupFactory>, IAsyncLifetime
{
    private readonly UiStartupFactory _factory;

    public AccountDetailsTests(UiStartupFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void OnInitializedAsync_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        const string userId = "4ce4ed46-cd2e-4529-88c0-57ed36e0f91b";
        const string accountName = "Gerald Ruben";
        
        var accessTokenProvider = new Mock<IAccessTokenProvider>();
        accessTokenProvider
            .Setup(x => x.RequestAccessToken())
            .ReturnsAsync(UiStartupFactory.GetFakeAccessTokenResult());
        
        accessTokenProvider
            .Setup(x => x.RequestAccessToken(It.IsAny<AccessTokenRequestOptions>()))
            .ReturnsAsync(UiStartupFactory.GetFakeAccessTokenResult());

        _factory.MockServer.Given(
            Request.Create().WithPath($"/authorized/Account/{userId}").UsingGet()
        ).RespondWith(
            Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type",
                    "application/json;charset=utf-8")
                .WithBody(GetApiAccountDetails()));

        using var ctx = new TestContext();
        ctx.Services.AddSuiteApp(_factory.GetConfiguration(), UiStartupFactory.GetHostEnvironment());
        ctx.Services.AddSingleton(accessTokenProvider.Object);
        var authContext = ctx.AddTestAuthorization();
        authContext.SetAuthorized("test@test.nl");
        authContext.SetRoles("None", "User","Admin");

        // Act
        var accountDetailsComponent = ctx.RenderComponent<AccountDetails>(
            parameters => 
                parameters.Add(p => p.Id, userId));
        accountDetailsComponent.WaitForState(() => accountDetailsComponent.Instance.IsReady);

        // Assert
        accountDetailsComponent
            .FindComponent<AccountDetails, RadzenCheckBox<bool>>("account-hasAdminRole")
            .Instance.Value.ShouldBe(true);
        
        accountDetailsComponent
            .FindComponent<AccountDetails, RadzenTextBox>("account-name")
            .Instance.Value.ShouldBe("Gerald Ruben");
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }

    private static string GetApiAccountDetails()
    {
        var response = new GetAccountResponse()
        {
            Account = new AccountDetailContract()
            {
                Id = "4ce4ed46-cd2e-4529-88c0-57ed36e0f91b",
                Name = "Gerald Ruben",
                Roles = new List<string>()
                {
                    "Admin",
                    "User",
                    "Guest"
                },
                IsResourceOwner = true
            }
        };
        
        return JsonSerializer.Serialize(response);
    }
}