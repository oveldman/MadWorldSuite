using System.Net;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Account;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;

namespace MadWorld.Backend.API.Anonymous.Functions.Blog;

public class GetBlogs
{
    private readonly IGetBlogsUseCase _useCase;

    public GetBlogs(IGetBlogsUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Function("GetBlogs")]
    [OpenApiOperation(operationId: "GetBlogs", tags: new[] { "Blog" })]
    [OpenApiParameter("page", Type = typeof(int))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetAccountResponse), Description = "The OK response")]
    public Result<GetBlogsResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Blog/page/{page}")] HttpRequestData request,
        FunctionContext executionContext,
        string page)
    {
        var getBlogsRequest = new GetBlogsRequest()
        {
            Page = page 
        };

        return _useCase.GetBlogs(getBlogsRequest);
    }
}