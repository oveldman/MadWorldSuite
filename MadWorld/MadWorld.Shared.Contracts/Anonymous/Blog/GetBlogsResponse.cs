namespace MadWorld.Shared.Contracts.Anonymous.Blog;

public class GetBlogsResponse
{
    public int Count { get; private set; }
    public IReadOnlyCollection<BlogContract> Blogs { get; private set; }
    
    public GetBlogsResponse(int count, IReadOnlyCollection<BlogContract> blogs)
    {
        Count = count;
        Blogs = blogs;
    }
}