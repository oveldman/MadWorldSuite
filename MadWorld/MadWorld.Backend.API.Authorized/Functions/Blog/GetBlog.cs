using System.Net;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Blog;

public class GetBlog
{
    private readonly IGetBlogUseCase _useCase;

    public GetBlog(IGetBlogUseCase useCase)
    {
        _useCase = useCase;
    }

    
    [Authorize(RoleTypes.Admin)]
    [Function("GetBlog")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "GetBlog", tags: new[] { "Blog" }, Summary = "Get details of a blog post")]
    [OpenApiParameter("id", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetBlogResponse), Description = "The OK response")]
    public Result<Option<GetBlogResponse>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Blog/{id}")] HttpRequestData request,
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