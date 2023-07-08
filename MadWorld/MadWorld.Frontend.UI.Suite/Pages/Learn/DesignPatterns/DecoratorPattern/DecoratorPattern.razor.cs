using LanguageExt;
using MadWorld.Frontend.Application.DesignPatterns.DecoratorPattern;

namespace MadWorld.Frontend.UI.Suite.Pages.Learn.DesignPatterns.DecoratorPattern;

public partial class DecoratorPattern
{
    private Option<ICharacter> _character = Option<ICharacter>.None;
    private ICharacter Character => _character.FirstOrDefault()!;
    
    private string _name = string.Empty;

    private void CreateCharacter(string name)
    {
        _character = new Character(name);
    }

    private void AddCleric()
    {
        _character = new Cleric(Character);
    }
    
    private void AddPaladin()
    {
        _character = new Paladin(Character);
    }
    
    private void AddWizard()
    {
        _character = new Wizard(Character);
    }
    
    private void Reset()
    {
        _character  = Option<ICharacter>.None;
    }
}