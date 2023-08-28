namespace MadWorld.Shared.Contracts.Authorized.Blog;

public class AddBlogRequest
{
    public ModifiableBlogContract? Blog { get; set; } = new();
}