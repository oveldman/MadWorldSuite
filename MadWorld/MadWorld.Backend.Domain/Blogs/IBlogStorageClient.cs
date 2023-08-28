using LanguageExt;
using LanguageExt.Common;

namespace MadWorld.Backend.Domain.Blogs;

public interface IBlogStorageClient
{
    Task<Result<bool>> UpsertPageAsBase64Async(string id, string body);
    Option<string> GetPageAsBase64(string id);
}