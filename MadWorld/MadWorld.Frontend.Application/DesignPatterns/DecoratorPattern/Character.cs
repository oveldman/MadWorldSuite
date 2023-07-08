namespace MadWorld.Frontend.Application.DesignPatterns.DecoratorPattern;

public class Character : ICharacter
{
    private readonly string _name;
    private readonly IReadOnlyList<string> _magicSpells;

    public Character(string name)
    {
        _name = name;
        _magicSpells = new[] { "Light" };
    }
    
    public string GetName()
    {
        return _name;
    }

    public string GetClass()
    {
        return string.Empty;
    }

    public IReadOnlyList<string> GetMagicSpells()
    {
        return _magicSpells;
    }
}