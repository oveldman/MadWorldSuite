using JetBrains.Annotations;
using MadWorld.ExternPackages.Monaco;
using MadWorld.ExternPackages.Monaco.Models;

namespace MadWorld.Frontend.UI.Suite.Pages.Tools;

[UsedImplicitly]
public partial class DefaultEditor
{
    internal bool IsReady { get; private set; }

    internal MonacoEditor MonacoEditor = default!;
    private readonly MonacoSettings _monacoSettings = new()
    {
        Language = Languages.PlainText
    };

    private void LoadEditor()
    {
        IsReady = true;
    }
    
    private void UnloadEditor()
    {
        IsReady = false;
    }
}