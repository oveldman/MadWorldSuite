using LanguageExt;
using LanguageExt.Common;

namespace MadWorld.Backend.Domain.Storage;

public interface IStorageClient
{
    Task<Result<Unit>> DeleteAsync(string blobName, string path);
    Option<string> GetBase64Body(string name, string path);
    Task<Result<Unit>> UpsertBase64Body(string blobName, string path, string body);
}