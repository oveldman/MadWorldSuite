using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Storage;

namespace MadWorld.Backend.Infrastructure.BlobStorage;

public class BlobStorageClient : IStorageClient
{
    private const string ContainerName = "madworld-storage";
    
    private readonly BlobContainerClient _client;

    public BlobStorageClient(BlobServiceClient serviceClient)
    {
        _client = serviceClient.GetBlobContainerClient(ContainerName);
        _client.CreateIfNotExists();
    }

    public async Task<Result<bool>> UpsertBase64Body(string blobName, string path, string body)
    {
        var blobClient = GetBlobClient(blobName, path);
        
        var bytes = Convert.FromBase64String(body);
        await blobClient.UploadAsync(BinaryData.FromBytes(bytes), overwrite: true);

        return true;
    }

    public Option<string> GetBase64Body(string name, string path)
    {
        var blobDownloadInfo = DownloadBlob(name, path);

        if (!blobDownloadInfo?.HasValue ?? true)
        {
            return Option<string>.None;
        }
        
        var bytes = new byte[blobDownloadInfo.Value.ContentLength];
        var readTotal = blobDownloadInfo.Value.Content.Read(bytes, 0, (int)blobDownloadInfo.Value.ContentLength);
        return readTotal != blobDownloadInfo.Value.ContentLength ? Option<string>.None : Convert.ToBase64String(bytes);
    }

    private Response<BlobDownloadInfo>? DownloadBlob(string blobName, string path)
    {
        var blobClient = GetBlobClient(blobName, path);
        return blobClient.Download();
    }
    
    private BlobClient GetBlobClient(string blobName, string path)
    {
        var fullName = Path.Combine(path, blobName);
        
        return _client.GetBlobClient(fullName);
    }
}