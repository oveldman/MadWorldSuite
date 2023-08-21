using LanguageExt;

namespace MadWorld.Backend.Domain.Blogs;

public interface IBlogStorageClient
{
    Option<string> GetPageAsBase64(string id);
}