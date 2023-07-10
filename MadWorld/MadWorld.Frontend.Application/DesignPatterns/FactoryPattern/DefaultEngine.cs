namespace MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

public class DefaultEngine : IEngine
{
    private readonly int _horsePower;
    private int HorsePower => _isStarted ? _horsePower : 0;
    private bool _isStarted;
    
    public DefaultEngine(int horsePower)
    {
        _horsePower = horsePower;
        _isStarted = false;
    }
    
    public void Start()
    {
        _isStarted = true;
    }
    
    public int GetMaxSpeed()
    {
        return HorsePower / 3;
    } 
}