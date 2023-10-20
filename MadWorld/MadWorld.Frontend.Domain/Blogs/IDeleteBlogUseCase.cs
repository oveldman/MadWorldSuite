namespace MadWorld.Frontend.Domain.Blogs;

public interface IDeleteBlogUseCase
{
    Task<bool> DeleteBlog(string id);
}