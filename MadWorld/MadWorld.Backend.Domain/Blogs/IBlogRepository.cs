using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Properties;

namespace MadWorld.Backend.Domain.Blogs;

public interface IBlogRepository
{
    IReadOnlyList<Blog> GetBlogs(int page);
    Option<Blog> GetBlog(GuidId id);
    Result<Unit> UpsertBlog(Blog blog);
}