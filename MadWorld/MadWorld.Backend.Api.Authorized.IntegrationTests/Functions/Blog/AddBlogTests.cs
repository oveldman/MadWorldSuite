using Azure.Data.Tables;
using Azure.Storage.Blobs;
using MadWorld.Backend.API.Authorized.Functions.Blog;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Backend.Infrastructure.BlobStorage;
using MadWorld.Backend.Infrastructure.BlobStorage.Blog;
using MadWorld.Backend.Infrastructure.TableStorage.Blogs;
using MadWorld.IntegrationTests.Extensions;
using MadWorld.Shared.Contracts.Authorized.Blog;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.Blog;

[Collection(CollectionTypes.IntegrationTests)]
public class AddBlogTests : IClassFixture<AuthorizedApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly AuthorizedApiStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    private readonly BlobServiceClient _blobServiceClient;
    
    private TableClient _tableClient = null!;
    private BlobContainerClient _blobClient = null!;
    private readonly AddBlog _function;

    public AddBlogTests(AuthorizedApiDockerStartupFactory factory)
    {
        _factory = factory;
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
        _blobServiceClient = factory.Host.Services.GetRequiredService<BlobServiceClient>();
        
        var useCase = factory.Host.Services.GetRequiredService<IAddBlogUseCase>();
        _function = new AddBlog(useCase);
        
    }

    [Fact]
    public async Task AddBlog_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        var request = new AddBlogRequest()
        {
            Blog = new ModifiableBlogContract()
            {
                Title = "What is the best framework?",
                Writer = "Mien Hieronymus",
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
        var contract = response.GetValue();
        contract.IsSuccess.ShouldBeTrue();

        var blog = _tableClient.Query<BlogEntity>().FirstOrDefault();
        blog.ShouldNotBeNull();
        blog.Writer.ShouldBe(request.Blog.Writer);
        blog.Title.ShouldBe(request.Blog.Title);
        
        var fileName = Path.Combine(BlogStorageClient.BlogPagePath, $"{blog.Identifier}.html");
        var blobFile = _blobClient.GetBlobClient(fileName);
        var result = await blobFile.DownloadContentAsync();
        result.Value.Content.ToString().ShouldBe("TestBody");
    }

    public async Task InitializeAsync()
    {
        await _tableServiceClient.CreateTableIfNotExistsAsync(BlogRepository.TableName);
        _tableClient = _tableServiceClient.GetTableClient(BlogRepository.TableName);
        
        _blobClient = _blobServiceClient.GetBlobContainerClient(BlobStorageClient.ContainerName);
        await _blobClient.CreateIfNotExistsAsync();
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}