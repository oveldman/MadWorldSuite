namespace MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

public class MercedesGarage : Garage
{
    private const int HorsePower = 1000; 
    
    protected override ICar CreateCar()
    {
        var engine = new DefaultEngine(HorsePower);
        var tires = new HardTires();

        return new Car(engine, tires);
    }
}