using LanguageExt.Common;
using MadWorld.Backend.Domain.Exceptions;

namespace MadWorld.Backend.Domain.General;

public sealed class Text
{
    private readonly string _text;
    
    private Text(string text)
    {
        _text = text;
    }
    
    public static Result<Text> Parse(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            return new Text(text);
        }

        return new Result<Text>(new ValidationException($"{nameof(text)} is not valid"));
    }

    public static implicit operator string(Text text) => text._text;
}