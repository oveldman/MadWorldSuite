namespace MadWorld.Frontend.Application.DesignPatterns.DecoratorPattern;

public interface ICharacter
{
    string GetName();
    string GetClass();
    IReadOnlyList<string> GetMagicSpells();
}