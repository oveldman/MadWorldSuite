using System.Text;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using MadWorld.Backend.API.Authorized.Functions.Blog;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Infrastructure.BlobStorage;
using MadWorld.Backend.Infrastructure.BlobStorage.Blog;
using MadWorld.Backend.Infrastructure.TableStorage.Blogs;
using MadWorld.Backend.Infrastructure.TableStorage.Extensions;
using MadWorld.IntegrationTests.Extensions;
using MadWorld.Shared.Contracts.Authorized.Blog;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.Blog;

[Collection(CollectionTypes.IntegrationTests)]
public class UpdateBlogTests : IClassFixture<AuthorizedApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly AuthorizedApiStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    private readonly BlobServiceClient _blobServiceClient;
    
    private TableClient _tableClient = null!;
    private BlobContainerClient _blobClient = null!;
    private readonly UpdateBlog _function;

    public UpdateBlogTests(AuthorizedApiDockerStartupFactory factory)
    {
        _factory = factory;
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
        _blobServiceClient = factory.Host.Services.GetRequiredService<BlobServiceClient>();
        
        var useCase = factory.Host.Services.GetRequiredService<IUpdateBlobUseCase>();
        _function = new UpdateBlog(useCase);
    }
    
    [Fact]
    public async Task UpdateBlog_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var request = new UpdateBlogRequest()
        {
            Id = "bbc13803-9995-463c-8c41-59093238444d",
            Blog = new ModifiableBlogContract()
            {
                Title = "What is the best framework?",
                Writer = "Alex Floris",
                Body = "VGVzdEJvZHk="
            }
        };

        var context = Substitute.For<FunctionContext>();
        var httpRequest = Substitute.For<HttpRequestData>(context);
        
        context.InstanceServices.Returns(_factory.Host.Services);
        httpRequest.Body.Returns(request.ToMemoryStream());
        
        // Act
        var response = await _function.Run(httpRequest, context);
        
        // Assert
        var contract = response.Match(
            option => option.Match(
                b => b,
                () => default!),
            _ => default!
        );
        contract.IsSuccess.ShouldBeTrue();

        var blog = _tableClient.Query<BlogEntity>().FirstOrDefault();
        blog.ShouldNotBeNull();
        blog.Identifier.ShouldBe(request.Id);
        blog.Writer.ShouldBe(request.Blog.Writer);
        blog.Title.ShouldBe(request.Blog.Title);
        
        var fileName = Path.Combine(BlogStorageClient.BlogPagePath, $"{request.Id}.html");
        var blobFile = _blobClient.GetBlobClient(fileName);
        var result = await blobFile.DownloadContentAsync();
        result.Value.Content.ToString().ShouldBe("TestBody");
    }
    
    public async Task InitializeAsync()
    {
        const string id = "bbc13803-9995-463c-8c41-59093238444d";
        const string content = "News Article Test";
        
        await _tableServiceClient.CreateTableIfNotExistsAsync(BlogRepository.TableName);
        _tableClient = _tableServiceClient.GetTableClient(BlogRepository.TableName);
        
        await _tableClient.AddEntityAsync(new BlogEntity()
        {
            RowKey = DateTimeUtc.Parse(new DateTime(2000, 10, 10)).ToRowKeyDesc(),
            Identifier = Guid.Parse(id).ToString(),
            Title = "What is the best framework?",
            Writer = "Mien Hieronymus",
            Created = DateTimeUtc.Now(),
            Updated = DateTimeUtc.Now(),
            IsDeleted = false
        });

        _blobClient = _blobServiceClient.GetBlobContainerClient(BlobStorageClient.ContainerName);
        await _blobClient.CreateIfNotExistsAsync();

        var fileName = Path.Combine(BlogStorageClient.BlogPagePath, $"{id}.html");
        var blobFile = _blobClient.GetBlobClient(fileName);
        
        var body = Encoding.UTF8.GetBytes(content);
        await blobFile.UploadAsync(BinaryData.FromBytes(body));
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}