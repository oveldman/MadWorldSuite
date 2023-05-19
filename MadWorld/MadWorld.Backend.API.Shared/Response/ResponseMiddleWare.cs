using System.Net;
using JetBrains.Annotations;
using LanguageExt;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace MadWorld.Backend.API.Shared.Response;

[UsedImplicitly]
public class ResponseMiddleWare : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        await next(context);

        var resultProcessed = context.GetInvocationResult();
        
        if (resultProcessed.Value is Result<Any> result)
        {   
            result.IfSucc(value => context.GetInvocationResult().Value = value);
            result.IfFail(exception => SetErrorResponse(exception, context).GetAwaiter().GetResult());
        }
    }
    
    private static async Task SetErrorResponse(Exception exception, FunctionContext context)
    {
        var request = await context.GetHttpRequestDataAsync();
        var newResponse = request!.CreateResponse();
        newResponse.StatusCode = HttpStatusCode.InternalServerError;
        await newResponse.WriteStringAsync(exception.Message);
        context.GetInvocationResult().Value = newResponse;
    }
}