using MadWorld.Frontend.Application.DesignPatterns.SingletonPattern;

namespace MadWorld.Frontend.UI.Suite.Pages.Learn.DesignPatterns.SingletonPattern;

public partial class SingletonPattern
{
    private Guid RaceId { get; set; } = Guid.Empty;
    
    private void GetRaceId()
    {
        var fia = Fia.Instance;
        RaceId = fia.GetRaceId();
    }

    private void ResetRaceId()
    {
        RaceId = Guid.Empty;
    }
}