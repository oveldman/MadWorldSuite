using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Blog;

namespace MadWorld.Frontend.Application.Blogs.Mapper;

public static class BlogMapper
{
    public static AddBlogRequest ToAddContract(BlogDetailContract blog, string bodyBase64)
    {
        return new AddBlogRequest()
        {
            Blog = ToModifiableContract(blog, bodyBase64)
        };
    }
    
    public static UpdateBlogRequest ToUpdateContract(BlogDetailContract blog, string bodyBase64)
    {
        return new UpdateBlogRequest()
        {
            Id = blog.Id,
            Blog = ToModifiableContract(blog, bodyBase64)
        };
    }
    
    private static ModifiableBlogContract ToModifiableContract(BlogDetailContract blog, string bodyBase64)
    {
        return new ModifiableBlogContract()
        {
            Title = blog.Title,
            Writer = blog.Writer,
            Body = bodyBase64
        };
    }
}