using Bunit;
using MadWorld.ExternPackages.Monaco.Dependencies;
using MadWorld.Frontend.UI.Shared.Dependencies;
using MadWorld.Frontend.UI.Suite.Pages.Tools;
using MadWorld.IntegrationTests.Startups;
using Shouldly;

namespace MadWorld.Frontend.UI.Suite.IntegrationTests.Pages.Tools;

public class DefaultEditorTests : IClassFixture<UiStartupFactory>, IAsyncLifetime
{
    private readonly UiStartupFactory _factory;

    public DefaultEditorTests(UiStartupFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public void OnInitializedAsync_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        using var ctx = new TestContext();
        ctx.Services.AddSuiteApp(_factory.GetConfiguration(), UiStartupFactory.GetHostEnvironment());
        ctx.Services.AddMonaco();
        
        // Act
        var defaultEditorComponent = ctx.RenderComponent<DefaultEditor>();
        ctx.JSInterop.SetupModule("./_content/MadWorld.ExternPackages.Monaco/monacoEditorInterop.js");
        ctx.JSInterop.Mode = JSRuntimeMode.Loose;
        defaultEditorComponent.Find("button").Click();
        defaultEditorComponent.WaitForState(() => defaultEditorComponent.Instance.IsReady);

        // Assert
        var editor = defaultEditorComponent.Instance.MonacoEditor;
        editor.ShouldNotBeNull();
        editor.EditorId.ShouldNotBeNull();
        defaultEditorComponent.Find("#" + editor.EditorId).ShouldNotBeNull();
    }

    public Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}