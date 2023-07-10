namespace MadWorld.Frontend.Application.DesignPatterns.SingletonPattern;

public class Fia
{
    private readonly Guid _raceId;
    
    private static readonly Lazy<Fia> FiaManagement 
        = new(() => new Fia());
    
    public static Fia Instance => FiaManagement.Value;

    private Fia()
    {
        _raceId = Guid.NewGuid();
    }
    
    public Guid GetRaceId()
    {
        return _raceId;
    }
}