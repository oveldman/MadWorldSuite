namespace MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

public abstract class Garage
{
    public ICar GetCar()
    {
        var car = CreateCar();
        car.Start();
        return car;
    }
    
    protected abstract ICar CreateCar();
}