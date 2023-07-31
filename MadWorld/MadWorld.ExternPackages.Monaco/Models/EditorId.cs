namespace MadWorld.ExternPackages.Monaco.Models;

public class EditorId
{
    private readonly Guid _id;
    
    public EditorId()
    {
        _id = new Guid();
    }

    public static implicit operator string(EditorId id)
    {
        var guidId = id._id
            .ToString()
            .Replace("_", "");

        return $"monaco-editor-{guidId}";
    }
}