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

public class AddBlog
{
    public AddBlog()
    {
    }

    [Authorize(RoleTypes.Admin)]
    [Function("AddBlog")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "AddBlog", tags: new[] { "Blog" })]
    [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(AddBlogRequest))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OkResponse), Description = "The OK response")]
    public async Task<OkResponse> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Blog")] HttpRequestData request,
        FunctionContext executionContext)
    {
        var addBlogRequest = await request.ReadFromJsonAsync<AddBlogRequest>();
        return new OkResponse();
    }
}