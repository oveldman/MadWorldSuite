using MadWorld.ExternPackages.Monaco;
using MadWorld.ExternPackages.Monaco.Models;

namespace MadWorld.Frontend.UI.Suite.Pages.Tools;

public partial class JsonEditor
{
    private MonacoEditor _monacoEditor = default!;
    private readonly MonacoSettings _monacoSettings = new()
    {
        Language = Languages.Json
    };
}