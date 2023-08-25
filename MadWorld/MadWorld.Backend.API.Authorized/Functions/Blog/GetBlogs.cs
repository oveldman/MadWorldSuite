using System.Net;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Blog;

public class GetBlogs
{
    private readonly IGetBlogsUseCase _useCase;

    public GetBlogs(IGetBlogsUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("GetBlogs")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "GetBlogs", tags: new[] { "Blog" }, Summary = "List all blog posts")]
    [OpenApiParameter("page", Type = typeof(int))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetBlogsResponse), Description = "The OK response")]
    public Result<GetBlogsResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Blogs/{page}")] HttpRequestData request,
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