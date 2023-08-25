namespace MadWorld.Shared.Contracts.Authorized.Blog;

public class UpdateBlogRequest
{
    public string Id { get; set; } = string.Empty;
    
    public EditBlogContract Blog { get; set; } = new();
}