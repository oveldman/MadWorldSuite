namespace MadWorld.Frontend.Application.DesignPatterns.DecoratorPattern;

public class Wizard : IClassDecorator
{
    private readonly ICharacter _character;
    
    private readonly IReadOnlyList<string> _magicSpells;

    public Wizard(ICharacter character)
    {
        _character = character;
        _magicSpells = new[] { "CounterSpell", "Fireball" };
    }
    
    public string GetName()
    {
        return _character.GetName();
    }
    
    public string GetClass()
    {
        return $"Wizard, {_character.GetClass()}";
    }

    public IReadOnlyList<string> GetMagicSpells()
    {
        return _character.GetMagicSpells()
            .Concat(_magicSpells)
            .ToList();
    }
}