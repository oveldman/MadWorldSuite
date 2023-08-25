namespace MadWorld.Shared.Contracts.Authorized.Blog;

public class AddBlogRequest
{
    public EditBlogContract Blog { get; set; } = new();
}