using System.Net;
using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Authorization;
using MadWorld.Shared.Contracts.Shared.Functions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Blog;

public class DeleteBlog
{
    public DeleteBlog()
    {
    }

    
    [Authorize(RoleTypes.Admin)]
    [Function("DeleteBlog")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "DeleteBlog", tags: new[] { "Blog" })]
    [OpenApiParameter("id", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OkResponse), Description = "The OK response")]
    public Result<OkResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Blog/{id}")] HttpRequestData request,
        FunctionContext executionContext,
        string id)
    {
        var deleteBlogRequest = new DeleteBlogRequest()
        {
            Id = id
        };

        return new OkResponse()
        {
            Message = $"Blog {deleteBlogRequest.Id} has been deleted"
        };
    }
}