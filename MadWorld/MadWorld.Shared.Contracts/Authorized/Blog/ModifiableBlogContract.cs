namespace MadWorld.Shared.Contracts.Authorized.Blog;

public class ModifiableBlogContract
{
    public string Title { get; set; } = null!;
    public string Writer { get; set; } = null!;
    public string Body { get; set; } = string.Empty;
}