using LanguageExt;
using LanguageExt.Common;

namespace MadWorld.Backend.Domain.Storage;

public interface IStorageClient
{
    Task<Result<bool>> UpsertBase64Body(string blobName, string path, string body);
    Option<string> GetBase64Body(string name, string path);
}