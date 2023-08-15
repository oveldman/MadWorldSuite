using System.Net;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
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
    public GetBlog()
    {
    }

    
    [Authorize(RoleTypes.Admin)]
    [Function("GetBlog")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "GetBlog", tags: new[] { "Blog" })]
    [OpenApiParameter("id", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetAccountResponse), Description = "The OK response")]
    public GetBlogResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Blog/{id}")] HttpRequestData request,
        FunctionContext executionContext,
        string id)
    {
        var getBlogRequest = new GetBlogRequest()
        {
            Id = id
        };

        return new GetBlogResponse()
        {
            Blog =
            {
                Id = "1",
                Title = "Title",
                Created = DateTime.Now,
                Updated = DateTime.Now,
                Writer = "D. Tester",
                Body = "Body"
            }
        };
    }
}