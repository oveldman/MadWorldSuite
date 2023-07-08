namespace MadWorld.Frontend.Application.DesignPatterns.DecoratorPattern;

public class Cleric : IClassDecorator
{
    private readonly ICharacter _character;
    
    private readonly IReadOnlyList<string> _magicSpells;

    public Cleric(ICharacter character)
    {
        _character = character;
        _magicSpells = new[] { "Bless", "Guiding Bolt" };
    }
    
    public string GetName()
    {
        return _character.GetName();
    }

    public string GetClass()
    {
        return $"Cleric, {_character.GetClass()}";
    }

    public IReadOnlyList<string> GetMagicSpells()
    {
        return _character.GetMagicSpells()
            .Concat(_magicSpells)
            .ToList();
    }
}