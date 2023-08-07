using MadWorld.ExternPackages.Monaco.Models;
using Microsoft.AspNetCore.Components;

namespace MadWorld.ExternPackages.Monaco;

public partial class MonacoEditor
{
    [Parameter]
    public int Height { get; set; } = 500;
    
    [Parameter]
    public MonacoSettings Settings { get; set; } = new();

    internal readonly EditorId EditorId = new();
    
    private string HeightPixels => Height + "px";
    
    [Inject]
    public MonacoManager MonacoManager { get; set; } = default!;

    public async Task SetValue(string value)
    {
        await MonacoManager.SetValue(value);
    }

    protected override async void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            await MonacoManager.Init(EditorId, Settings);
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}