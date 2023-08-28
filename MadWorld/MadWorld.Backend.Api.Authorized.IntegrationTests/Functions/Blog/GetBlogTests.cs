using System.Text;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using FluentAssertions;
using MadWorld.Backend.API.Authorized.Functions.Blog;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Infrastructure.BlobStorage;
using MadWorld.Backend.Infrastructure.BlobStorage.Blog;
using MadWorld.Backend.Infrastructure.TableStorage.Blogs;
using MadWorld.Backend.Infrastructure.TableStorage.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.Blog;

[Collection(CollectionTypes.IntegrationTests)]
public class GetBlogTests : IClassFixture<AuthorizedApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly AuthorizedApiStartupFactory _factory;
    private readonly BlobServiceClient _blobServiceClient;
    private readonly TableServiceClient _tableServiceClient;
    private readonly GetBlog _function;

    public GetBlogTests(AuthorizedApiDockerStartupFactory factory)
    {
        _factory = factory;
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
        _blobServiceClient = factory.Host.Services.GetRequiredService<BlobServiceClient>();
        
        var useCase = factory.Host.Services.GetRequiredService<IGetBlogUseCase>();
        _function = new GetBlog(useCase);
    }

    [Fact]
    public void GetBlog_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        const string id = "33a8b830-2689-4351-8473-68d97edacd0c";
        
        var context = Substitute.For<FunctionContext>();
        var request = Substitute.For<HttpRequestData>(context);
        
        // Act
        var response = _function.Run(request, context, id);
        
        // Assert
        var contract = response.Match(
            option => option.Match(
                b => b,
                () => default!),
            _ => default!
        );

        contract.Blog.Id.ShouldBe(id);
        contract.Blog.Body.ShouldBe("VGVzdEJvZHk=");
    }

    public async Task InitializeAsync()
    {
        const string id = "33a8b830-2689-4351-8473-68d97edacd0c";
        const string content = "TestBody";
        
        await _tableServiceClient.CreateTableIfNotExistsAsync(BlogRepository.TableName);
        var client = _tableServiceClient.GetTableClient(BlogRepository.TableName);
        
        await client.AddEntityAsync(new BlogEntity()
        {
            RowKey = DateTimeUtc.Parse(new DateTime(2000, 10, 10)).ToRowKeyDesc(),
            Identifier = Guid.Parse(id).ToString(),
            Title = "What is the best framework?",
            Writer = "Mien Hieronymus",
            Created = DateTimeUtc.Now(),
            Updated = DateTimeUtc.Now(),
            IsDeleted = false
        });

        var blobService = _blobServiceClient.GetBlobContainerClient(BlobStorageClient.ContainerName);
        await blobService.CreateIfNotExistsAsync();

        var fileName = Path.Combine(BlogStorageClient.BlogPagePath, $"{id}.html");
        var blobFile = blobService.GetBlobClient(fileName);
        
        var body = Encoding.UTF8.GetBytes(content);
        await blobFile.UploadAsync(BinaryData.FromBytes(body));
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}