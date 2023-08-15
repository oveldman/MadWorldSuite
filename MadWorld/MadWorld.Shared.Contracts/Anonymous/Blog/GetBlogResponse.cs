namespace MadWorld.Shared.Contracts.Anonymous.Blog;

public class GetBlogResponse
{
    public BlogDetailContract Blog { get; private set; } = new();
}