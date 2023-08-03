using MadWorld.ExternPackages.Monaco;
using MadWorld.ExternPackages.Monaco.Models;

namespace MadWorld.Frontend.UI.Suite.Pages.Tools;

public partial class DefaultEditor
{
    private bool _isReady;
    
    private MonacoEditor _monacoEditor = default!;
    private readonly MonacoSettings _monacoSettings = new()
    {
        Language = Languages.PlainText
    };

    private void LoadEditor()
    {
        _isReady = true;
    }
    
    private void UnloadEditor()
    {
        _isReady = false;
    }
}