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
    
    public async ValueTask Init(string editorId)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("init", editorId);
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