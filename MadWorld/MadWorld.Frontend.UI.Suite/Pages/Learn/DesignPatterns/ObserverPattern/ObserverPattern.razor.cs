using JetBrains.Annotations;
using MadWorld.Frontend.Application.DesignPatterns.ObserverPattern;

namespace MadWorld.Frontend.UI.Suite.Pages.Learn.DesignPatterns.ObserverPattern;

[UsedImplicitly]
public partial class ObserverPattern
{
    private readonly FormulaOne _formulaOne = new();
    private readonly List<ICar> _formulaOneCars = new();

    private void ChangeFlag(RacingFlag flag)
    {
        _formulaOne.SetRacingFlag(flag);
    }

    private void AddRedBull()
    {
        _formulaOneCars.Add(new RedBull(_formulaOne));
    }

    private void AddMercedes()
    {
        _formulaOneCars.Add(new Mercedes(_formulaOne));
    }

    private void RemoveCar(ICar car)
    {
        _formulaOneCars.Remove(car);
        car.Dispose();
    }
}