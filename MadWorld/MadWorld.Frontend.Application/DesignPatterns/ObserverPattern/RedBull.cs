namespace MadWorld.Frontend.Application.DesignPatterns.ObserverPattern;

public class RedBull : ICar, IObserver
{
    public string Name { get; } = "Red Bull";
    
    private RacingFlag _flag;
    private readonly FormulaOne _formulaOne;

    public RedBull(FormulaOne formulaOne)
    {
        _formulaOne = formulaOne;
        _formulaOne.RegisterObserver(this);
    }

    public void Update()
    {
        _flag = _formulaOne.GetRacingFlag();
    }

    public string GetRacingPace()
    {
        return _flag switch
        {
            RacingFlag.Green => "Fly away!",
            RacingFlag.Yellow => "Go half speed!",
            RacingFlag.Red => "Stop!",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public void Dispose()
    {
        _formulaOne.UnregisterObserver(this);
        GC.SuppressFinalize(this);
    }
}