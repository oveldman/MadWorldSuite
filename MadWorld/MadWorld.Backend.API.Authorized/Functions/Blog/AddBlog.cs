using System.Net;
using LanguageExt.Common;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Backend.API.Shared.OpenAPI;
using MadWorld.Backend.Application.Blogs;
using MadWorld.Backend.Domain.Blogs;
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
    private readonly IAddBlogUseCase _useCase;

    public AddBlog(IAddBlogUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Authorize(RoleTypes.Admin)]
    [Function("AddBlog")]
    [OpenApiSecurity(Security.SchemeName, SecuritySchemeType.ApiKey, Name = Security.HeaderName, In = OpenApiSecurityLocationType.Header)]
    [OpenApiOperation(operationId: "AddBlog", tags: new[] { "Blog" }, Summary = "Create a new blog post")]
    [OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(AddBlogRequest))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(OkResponse), Description = "The OK response")]
    public async Task<Result<OkResponse>> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Blog")] HttpRequestData request,
        FunctionContext executionContext)
    {
        var user = executionContext.GetUser();
        
        var addBlogRequest = await request.ReadFromJsonAsync<AddBlogRequest>();
        return _useCase.AddBlob(addBlogRequest, user.Name);
    }
}