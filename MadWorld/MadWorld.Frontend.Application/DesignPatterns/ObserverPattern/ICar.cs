namespace MadWorld.Frontend.Application.DesignPatterns.ObserverPattern;

public interface ICar : IDisposable
{
    public string Name { get; }
    string GetRacingPace();
}