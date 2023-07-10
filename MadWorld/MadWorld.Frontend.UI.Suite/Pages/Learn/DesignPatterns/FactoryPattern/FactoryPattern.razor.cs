using JetBrains.Annotations;
using MadWorld.Frontend.Application.DesignPatterns.FactoryPattern;

namespace MadWorld.Frontend.UI.Suite.Pages.Learn.DesignPatterns.FactoryPattern;

[UsedImplicitly]
public partial class FactoryPattern
{
    private Garage RedBullGarage { get; } = new RedBullGarage();
    private Garage MercedesGarage { get; } = new MercedesGarage();
    
    private ICar? RedBullCar { get; set; }
    private ICar? MercedesCar { get; set; }

    private void GetRedBullCar()
    {
        RedBullCar = RedBullGarage.GetCar();
    }

    private void GetMercedesCar()
    {
        MercedesCar = MercedesGarage.GetCar();
    }
}