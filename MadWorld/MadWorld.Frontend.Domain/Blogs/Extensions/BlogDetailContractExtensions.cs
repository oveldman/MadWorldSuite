using System.Text;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Frontend.Domain.Blogs.Extensions;

public static class BlogDetailContractExtensions
{
    public static string GetUtf8Body(this BlogDetailContract blog)
    {
        return Encoding.UTF8.GetString(Convert.FromBase64String(blog.BodyBase64));
    }
}