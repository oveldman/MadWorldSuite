using System.Net;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Account;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace MadWorld.Backend.API.Anonymous.Functions.Blog;

public class GetBlog
{
    private readonly IGetBlogUseCase _useCase;

    public GetBlog(IGetBlogUseCase useCase)
    {
        _useCase = useCase;
    }

    
    [Function("GetBlog")]
    [OpenApiOperation(operationId: "GetBlog", tags: new[] { "Blog" })]
    [OpenApiParameter("id", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetAccountResponse), Description = "The OK response")]
    public Result<GetBlogResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Blog/{id}")] HttpRequestData request,
        FunctionContext executionContext,
        string id)
    {
        var getBlogRequest = new GetBlogRequest()
        {
            Id = id
        };

        return _useCase.GetBlog(getBlogRequest);
    }
}