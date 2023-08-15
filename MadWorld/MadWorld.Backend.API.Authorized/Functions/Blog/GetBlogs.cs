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
using Microsoft.Graph.Models.IdentityGovernance;
using Microsoft.OpenApi.Models;

namespace MadWorld.Backend.API.Authorized.Functions.Blog;

public class GetBlogs
{
    public GetBlogs()
    {
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("GetBlogs")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "GetBlogs", tags: new[] { "Blog" })]
    [OpenApiParameter("page", Type = typeof(int))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(GetAccountResponse), Description = "The OK response")]
    public GetBlogsResponse Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Blog/page/{page}")] HttpRequestData request,
        FunctionContext executionContext,
        string page)
    {
        var getBlogsRequest = new GetBlogsRequest()
        {
            Page = page 
        };
        
        return new GetBlogsResponse(Array.Empty<BlogContract>());
    }
}