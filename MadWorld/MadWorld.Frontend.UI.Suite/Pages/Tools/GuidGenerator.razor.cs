using MadWorld.ExternPackages.Monaco;

namespace MadWorld.Frontend.UI.Suite.Pages.Tools;

public partial class GuidGenerator
{
    private const int MinGuidAmount = 0;
    private const int MaxGuidAmount = 10000;
    
    private MonacoEditor _monacoEditor = default!;

    private int _guidAmountSelected = 1;
    
    private async Task GenerateGuids()
    {
        ValidateAmountOfGuids();
        
        var guid = GenerateGuidsToString();
        await _monacoEditor.SetValue(guid);
    }

    private string GenerateGuidsToString()
    {
        var guids = string.Empty;
        
        for (var i = 0; i < _guidAmountSelected; i++)
        {
            guids += Guid.NewGuid() + "\n";
        }

        return guids;
    }

    private void ValidateAmountOfGuids()
    {
        if (_guidAmountSelected < MinGuidAmount)
        {
            _guidAmountSelected = MinGuidAmount;
        }

        if (_guidAmountSelected > MaxGuidAmount)
        {
            _guidAmountSelected = MaxGuidAmount;
        }
    }
}