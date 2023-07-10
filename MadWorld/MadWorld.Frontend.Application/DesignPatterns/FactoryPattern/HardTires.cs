namespace MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

public class HardTires : ITires
{
    public double GetMultiplier()
    {
        return 1.0;
    }
}