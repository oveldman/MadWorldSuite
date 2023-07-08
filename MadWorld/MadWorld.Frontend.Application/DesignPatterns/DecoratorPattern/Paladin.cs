namespace MadWorld.Frontend.Application.DesignPatterns.DecoratorPattern;

public class Paladin : IClassDecorator
{
    private readonly ICharacter _character;
    
    private readonly IReadOnlyList<string> _magicSpells;

    public Paladin(ICharacter character)
    {
        _character = character;
        _magicSpells = new[] { "Command", "CloudKill" };
    }
    
    public string GetName()
    {
        return _character.GetName();
    }
    
    public string GetClass()
    {
        return $"Paladin, {_character.GetClass()}";
    }

    public IReadOnlyList<string> GetMagicSpells()
    {
        return _character.GetMagicSpells()
            .Concat(_magicSpells)
            .ToList();
    }
}