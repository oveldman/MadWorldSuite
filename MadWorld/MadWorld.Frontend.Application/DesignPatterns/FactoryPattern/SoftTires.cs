namespace MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

public class SoftTires : ITires
{
    public double GetMultiplier()
    {
        return 1.1;
    }
}