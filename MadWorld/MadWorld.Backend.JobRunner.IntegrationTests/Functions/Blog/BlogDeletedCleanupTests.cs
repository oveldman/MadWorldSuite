using System.Text;
using Azure.Data.Tables;
using Azure.Storage.Blobs;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Domain.System;
using MadWorld.Backend.Infrastructure.BlobStorage;
using MadWorld.Backend.Infrastructure.BlobStorage.Blog;
using MadWorld.Backend.Infrastructure.TableStorage.Blogs;
using MadWorld.Backend.Infrastructure.TableStorage.Extensions;
using MadWorld.Backend.JobRunner.Functions.Blog;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Shouldly;

namespace MadWorld.Backend.JobRunner.IntegrationTests.Functions.Blog;

public class BlogDeletedCleanupTests : IClassFixture<JobRunnerDockerStartupFactory>, IAsyncLifetime
{
    private readonly JobRunnerDockerStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    private readonly BlobServiceClient _blobServiceClient;
    
    private TableClient _tableClient = null!;
    private BlobContainerClient _blobClient = null!;
    private readonly BlogDeletedCleanup _function;
    
    public BlogDeletedCleanupTests(JobRunnerDockerStartupFactory factory)
    {
        _factory = factory;
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
        _blobServiceClient = factory.Host.Services.GetRequiredService<BlobServiceClient>();
        
        var useCase = factory.Host.Services.GetRequiredService<IBlogDeletedCleanupUseCase>();
        _function = new BlogDeletedCleanup(useCase);
    }

    [Fact]
    public async Task BlogDeletedCleanup_Regularly_ShouldReturnExpectedResult()
    {
        const string idToDelete = "33fbe40b-ec0a-4c3d-87de-7604b524c4ce";
        
        // Arrange
        var context = Substitute.For<FunctionContext>();
        
        // Act
        await _function.Run(context);
        
        // Assert
        var blogs = _tableClient.Query<BlogEntity>();
        blogs.Count().ShouldBe(2);
        blogs.ShouldNotContain(b => b.Identifier == idToDelete);

        var fileName = Path.Combine(BlogStorageClient.BlogPagePath, $"{idToDelete}.html");
        var blobFile = _blobClient.GetBlobClient(fileName);
        var blobExists = await blobFile.ExistsAsync();
        blobExists.Value.ShouldBeFalse();
    }

    public async Task InitializeAsync()
    {
        SystemTime.SetDateTime(new DateTime(2023, 09, 01));
        
        await _tableServiceClient.CreateTableIfNotExistsAsync(BlogRepository.TableName);
        _tableClient = _tableServiceClient.GetTableClient(BlogRepository.TableName);
        await _tableClient.AddEntityAsync(new BlogEntity()
        {
            RowKey = DateTimeUtc.Parse(new DateTime(2023, 06, 10)).ToRowKeyDesc(),
            Identifier = Guid.Parse("935eedd2-be0e-4308-aaa5-bd5bc7d5dceb").ToString(),
            Title = "How to create unittest",
            Writer = "Alex Floris",
            Created = DateTimeUtc.Parse(new DateTime(2023, 06, 10)),
            Updated = DateTimeUtc.Parse(new DateTime(2023, 06, 10)),
            IsDeleted = false
        });
        
        await _tableClient.AddEntityAsync(new BlogEntity()
        {
            RowKey = DateTimeUtc.Parse(new DateTime(2023, 08, 10)).ToRowKeyDesc(),
            Identifier = Guid.Parse("33a8b830-2689-4351-8473-68d97edacd0c").ToString(),
            Title = "What is the best framework?",
            Writer = "Mien Hieronymus",
            Created = DateTimeUtc.Parse(new DateTime(2023, 08, 10)),
            Updated = DateTimeUtc.Parse(new DateTime(2023, 08, 20)),
            IsDeleted = true
        });

        const string idToDelete = "33fbe40b-ec0a-4c3d-87de-7604b524c4ce";
        const string content = "TestBody";
        
        await _tableClient.AddEntityAsync(new BlogEntity()
        {
            RowKey = DateTimeUtc.Parse(new DateTime(2023, 04, 10)).ToRowKeyDesc(),
            Identifier = Guid.Parse(idToDelete).ToString(),
            Title = "How to improve your code?",
            Writer = "Mien Hieronymus",
            Created = DateTimeUtc.Parse(new DateTime(2023, 04, 10)),
            Updated = DateTimeUtc.Parse(new DateTime(2023, 05, 10)),
            IsDeleted = true
        });
        
        _blobClient = _blobServiceClient.GetBlobContainerClient(BlobStorageClient.ContainerName);
        await _blobClient.CreateIfNotExistsAsync();

        var fileName = Path.Combine(BlogStorageClient.BlogPagePath, $"{idToDelete}.html");
        var blobFile = _blobClient.GetBlobClient(fileName);
        
        var body = Encoding.UTF8.GetBytes(content);
        await blobFile.UploadAsync(BinaryData.FromBytes(body));
    }

    public async Task DisposeAsync()
    {
        SystemTime.ResetDateTime();
        
        await _factory.DisposeAsync();
    }
}