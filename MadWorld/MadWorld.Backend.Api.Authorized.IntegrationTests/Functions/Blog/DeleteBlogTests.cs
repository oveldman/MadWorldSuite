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
public class DeleteBlogTests : IClassFixture<AuthorizedApiDockerStartupFactory>, IAsyncLifetime
{
    private readonly DateTimeUtc _blogCreated = DateTimeUtc.Parse(new DateTime(2010, 10, 10));
    
    private readonly AuthorizedApiStartupFactory _factory;
    private readonly TableServiceClient _tableServiceClient;
    private readonly DeleteBlog _function;

    public DeleteBlogTests(AuthorizedApiDockerStartupFactory factory)
    {
        _factory = factory;
        _tableServiceClient = factory.Host.Services.GetRequiredService<TableServiceClient>();
        
        var useCase = factory.Host.Services.GetRequiredService<IDeleteBlogUseCase>();
        _function = new DeleteBlog(useCase);
    }

    [Fact]
    public void DeleteBlog_Regularly_ShouldReturnExpectedResult()
    {
        // Arrange
        const string id = "935eedd2-be0e-4308-aaa5-bd5bc7d5dceb";
        
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
        
        contract.IsSuccess.ShouldBeTrue();
        
        var client = _tableServiceClient.GetTableClient(BlogRepository.TableName);
        var entity = client.GetEntity<BlogEntity>(
            BlogEntity.PartitionKeyName,
            _blogCreated.ToRowKeyDesc());
        
        entity.Value.Identifier.ShouldBe(id);
        entity.Value.IsDeleted.ShouldBeTrue();
    }

    public Task InitializeAsync()
    {
        _tableServiceClient.CreateTableIfNotExists(BlogRepository.TableName);
        var client = _tableServiceClient.GetTableClient(BlogRepository.TableName);
        client.AddEntity(new BlogEntity()
        {
            RowKey = _blogCreated.ToRowKeyDesc(),
            Identifier = Guid.Parse("935eedd2-be0e-4308-aaa5-bd5bc7d5dceb").ToString(),
            Title = "How to create unittest",
            Writer = "Alex Floris",
            Created = _blogCreated,
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