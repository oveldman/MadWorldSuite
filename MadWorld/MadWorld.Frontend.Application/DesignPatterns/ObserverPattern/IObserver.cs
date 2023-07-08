namespace MadWorld.Frontend.Application.DesignPatterns.ObserverPattern;

public interface IObserver : IDisposable
{
    void Update();
}