using MadWorld.ExternPackages.Monaco.Models;
using Microsoft.AspNetCore.Components;

namespace MadWorld.ExternPackages.Monaco;

public partial class MonacoEditor
{
    [Parameter]
    public int Height { get; set; } = 500;

    private readonly EditorId _editorId = new();
    
    private string HeightPixels => Height + "px";
    
    [Inject]
    public MonacoManager MonacoManager { get; set; } = default!;

    protected override async void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            await MonacoManager.Init(_editorId);
        }

        await base.OnAfterRenderAsync(firstRender);
    }
}