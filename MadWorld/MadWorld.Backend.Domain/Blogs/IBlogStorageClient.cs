using LanguageExt;
using LanguageExt.Common;

namespace MadWorld.Backend.Domain.Blogs;

public interface IBlogStorageClient
{
    Task<Result<Unit>> DeletePageAsync(string id);
    Option<string> GetPageAsBase64(string id);
    Task<Result<Unit>> UpsertPageAsBase64Async(string id, string body);
}