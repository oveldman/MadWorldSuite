using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MadWorld.ExternPackages.Monaco.Models;
using Microsoft.JSInterop;

namespace MadWorld.ExternPackages.Monaco;

public class MonacoManager : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public MonacoManager(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/MadWorld.ExternPackages.Monaco/monacoEditorInterop.js").AsTask());
    }
    
    public async ValueTask Init(string editorId, MonacoSettings settings)
    {
        var module = await moduleTask.Value;

        var monacoSettings = JsonSerializer.Serialize(settings);
        await module.InvokeVoidAsync("init", editorId, monacoSettings);
    }

    public async ValueTask SetValue(string value)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("setValue", value);
    }
    
    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}