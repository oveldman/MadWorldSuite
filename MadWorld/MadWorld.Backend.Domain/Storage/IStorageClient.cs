using LanguageExt;

namespace MadWorld.Backend.Domain.Storage;

public interface IStorageClient
{
    Option<string> GetBase64Body(string name, string path);
}