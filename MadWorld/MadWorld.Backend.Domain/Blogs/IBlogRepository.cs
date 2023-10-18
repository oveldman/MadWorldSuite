using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Properties;

namespace MadWorld.Backend.Domain.Blogs;

public interface IBlogRepository
{
    Task<int> CountBlogs();
    Result<Unit> DeleteBlog(Blog blog);
    IReadOnlyList<Blog> GetBlogs(int page);
    Option<Blog> GetBlog(GuidId id);
    IReadOnlyList<Blog> GetDeletedBlogs();
    Result<Unit> UpsertBlog(Blog blog);
}