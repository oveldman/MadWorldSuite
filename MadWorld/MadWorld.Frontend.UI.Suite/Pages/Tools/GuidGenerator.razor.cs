using System.Text;
using JetBrains.Annotations;
using MadWorld.ExternPackages.Monaco;
using MadWorld.ExternPackages.Monaco.Models;

namespace MadWorld.Frontend.UI.Suite.Pages.Tools;

[UsedImplicitly]
public partial class GuidGenerator
{
    private const int MinGuidAmount = 0;
    private const int MaxGuidAmount = 10_000;
    
    private MonacoEditor _monacoEditor = default!;
    private readonly MonacoSettings _monacoSettings = new();

    private int _guidAmountSelected = 1;
    
    private async Task GenerateGuids()
    {
        ValidateAmountOfGuids();
        
        var guid = GenerateGuidsToString();
        await _monacoEditor.SetValue(guid);
    }

    private string GenerateGuidsToString()
    {
        var guidBuilder = new StringBuilder(string.Empty, SelectedGuidAmountOfCharacters);
        
        for (var i = 0; i < _guidAmountSelected; i++)
        {
            guidBuilder.AppendLine($"{Guid.NewGuid()}");
        }

        return guidBuilder.ToString();
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

    private const int GuidAmountOfCharacters = 36;
    private int SelectedGuidAmountOfCharacters => GuidAmountOfCharacters * _guidAmountSelected;
}