using System.Net;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Authorized.Blog;
using MadWorld.Shared.Contracts.Shared.Authorization;
using MadWorld.Shared.Contracts.Shared.Functions;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Blog;

public class UpdateBlog
{
    public UpdateBlog()
    {
    }

    [Authorize(RoleTypes.Admin)]
    [Function("UpdateBlog")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "UpdateBlog", tags: new[] { "Blog" })]
    [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(UpdateBlogRequest))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OkResponse), Description = "The OK response")]
    public async Task<OkResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "patch", Route = "Blog")] HttpRequestData request,
        FunctionContext executionContext)
    {
        var updateBlogRequest = await request.ReadFromJsonAsync<UpdateBlogRequest>();
        return new OkResponse();
    }
}