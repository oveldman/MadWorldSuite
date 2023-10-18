using Azure.Data.Tables;
using MadWorld.Backend.API.Authorized.Functions.Blog;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Properties;
using MadWorld.Backend.Infrastructure.TableStorage.Blogs;
using MadWorld.Backend.Infrastructure.TableStorage.Extensions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MadWorld.Backend.Api.Authorized.IntegrationTests.Functions.Blog;

[Collection(CollectionTypes.IntegrationTests)]
public class GetBlogsTests : IClassFixture<AuthorizedApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly AuthorizedApiStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    private readonly GetBlogs _function;

    public GetBlogsTests(AuthorizedApiDockerStartupFactory factory)
    {
        _factory = factory;
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
        
        var useCase = factory.Host.Services.GetRequiredService<IGetBlogsUseCase>();
        _function = new GetBlogs(useCase);
    }
    
    [Fact]
    public async Task GetBlogs_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        const string page = "0";
        
        var context = Substitute.For<FunctionContext>();
        var request = Substitute.For<HttpRequestData>(context);
        
        // Act
        var response = await _function.Run(request, context, page);
        
        // Assert
        var contract = response.Match(
            b => b, 
            _ => default!);
        
        contract.Count.ShouldBe(2);
        contract.Blogs.Count.ShouldBe(2);
        contract.Blogs.First().Id.ShouldBe("935eedd2-be0e-4308-aaa5-bd5bc7d5dceb");
        contract.Blogs.First().Title.ShouldBe("How to create unittest");
        contract.Blogs.First().Writer.ShouldBe("Alex Floris");
    }

    public Task InitializeAsync()
    {
        _tableServiceClient.CreateTableIfNotExists(BlogRepository.TableName);
        var client = _tableServiceClient.GetTableClient(BlogRepository.TableName);
        client.AddEntity(new BlogEntity()
        {
            RowKey = DateTimeUtc.Parse(new DateTime(2010, 10, 10)).ToRowKeyDesc(),
            Identifier = Guid.Parse("935eedd2-be0e-4308-aaa5-bd5bc7d5dceb").ToString(),
            Title = "How to create unittest",
            Writer = "Alex Floris",
            Created = DateTimeUtc.Now(),
            Updated = DateTimeUtc.Now(),
            IsDeleted = false
        });
        
        client.AddEntity(new BlogEntity()
        {
            RowKey = DateTimeUtc.Parse(new DateTime(2000, 10, 10)).ToRowKeyDesc(),
            Identifier = Guid.Parse("33a8b830-2689-4351-8473-68d97edacd0c").ToString(),
            Title = "What is the best framework?",
            Writer = "Mien Hieronymus",
            Created = DateTimeUtc.Now(),
            Updated = DateTimeUtc.Now(),
            IsDeleted = false
        });
        
        return Task.CompletedTask;
    }

    public async Task DisposeAsync()
    {
        await _factory.DisposeAsync();
    }
}