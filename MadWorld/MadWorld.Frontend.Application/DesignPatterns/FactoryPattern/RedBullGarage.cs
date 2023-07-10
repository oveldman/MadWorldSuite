namespace MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

public class RedBullGarage : Garage
{
    private const int HorsePower = 1100; 
    
    protected override ICar CreateCar()
    {
        var engine = new DefaultEngine(HorsePower);
        var tires = new SoftTires();

        return new Car(engine, tires);
    }
}