namespace MadWorld.Shared.Contracts.Anonymous.Blog;

public class GetBlogsResponse
{
    public IReadOnlyCollection<BlogContract> Blogs { get; private set; }
    
    public GetBlogsResponse(IReadOnlyCollection<BlogContract> blogs)
    {
        Blogs = blogs;
    }
}