namespace MadWorld.Frontend.Application.DesignPatterns.ObserverPattern;

public class Mercedes : ICar, IObserver
{
    public string Name { get; } = "Mercedes";
    
    private RacingFlag _flag;
    private readonly FormulaOne _formulaOne;

    public Mercedes(FormulaOne formulaOne)
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
            RacingFlag.Green => "Full speed ahead!",
            RacingFlag.Yellow => "Slow down!",
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