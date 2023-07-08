namespace MadWorld.Frontend.Application.DesignPatterns.ObserverPattern;

public class FormulaOne : ISubject
{
    private readonly List<IObserver> _observers = new();
    private RacingFlag _racingFlag = RacingFlag.Green;
    
    public void RegisterObserver(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        _observers.ForEach(o => o.Update());
    }
    
    public RacingFlag GetRacingFlag()
    {
        return _racingFlag;
    }
    
    public void SetRacingFlag(RacingFlag racingFlag)
    {
        _racingFlag = racingFlag;
        NotifyObservers();
    }
}