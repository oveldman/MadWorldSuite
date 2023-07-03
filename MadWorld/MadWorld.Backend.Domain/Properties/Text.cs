using LanguageExt.Common;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Backend.Domain.LanguageExt;

namespace MadWorld.Backend.Domain.Properties;

public sealed class Text : ValueObject
{
    private readonly string _text;
    
    private Text(string text)
    {
        _text = text;
    }
    
    public static Result<Text> Parse(string text)
    {
        return !string.IsNullOrWhiteSpace(text) 
            ? new Text(text) 
            : new Result<Text>(new ValidationException($"{nameof(text)} is not valid"));
    }

    public static implicit operator string(Text text) => text._text;
    
    public static explicit operator Text(string text) 
        => new(text);
}