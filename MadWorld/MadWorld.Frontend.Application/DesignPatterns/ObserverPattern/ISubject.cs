namespace MadWorld.Frontend.Application.DesignPatterns.ObserverPattern;

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void UnregisterObserver(IObserver observer);
    void NotifyObservers();
}