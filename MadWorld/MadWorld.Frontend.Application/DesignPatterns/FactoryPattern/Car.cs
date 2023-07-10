namespace MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

public class Car : ICar
{
    private readonly IEngine _engine;
    private readonly ITires _tires;
    
    public Car(IEngine engine, ITires tires)
    {
        _engine = engine;
        _tires = tires;
    }
    
    public void Start()
    {
        _engine.Start();
    }

    public double GetMaxSpeed()
    {
        return _engine.GetMaxSpeed() * _tires.GetMultiplier();
    }
}