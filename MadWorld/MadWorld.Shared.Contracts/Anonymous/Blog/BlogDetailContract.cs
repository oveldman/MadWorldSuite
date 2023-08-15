namespace MadWorld.Shared.Contracts.Anonymous.Blog;

public class BlogDetailContract
{
    public string Id { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Writer { get; set; } = null!;
    public DateTime Created { get; set; } = DateTime.MinValue;
    public DateTime Updated { get; set; } = DateTime.MinValue;

    public string Body { get; set; } = string.Empty;
}