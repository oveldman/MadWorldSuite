namespace MadWorld.ExternPackages.Monaco.Models;

public class EditorId
{
    private readonly Guid _id;
    
    public EditorId()
    {
        _id = Guid.NewGuid();
    }

    public static implicit operator string(EditorId id)
    {
        var guidId = id._id
            .ToString()
            .Replace("-", "");

        return $"monaco-editor-{guidId}";
    }
}