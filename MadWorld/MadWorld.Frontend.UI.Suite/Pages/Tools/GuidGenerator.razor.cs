using MadWorld.ExternPackages.Monaco;

namespace MadWorld.Frontend.UI.Suite.Pages.Tools;

public partial class GuidGenerator
{
    private MonacoEditor _monacoEditor = default!;
    
    private async Task GenerateGuid()
    {
        var guid = Guid.NewGuid().ToString();
        await _monacoEditor.SetValue(guid);
    }
}