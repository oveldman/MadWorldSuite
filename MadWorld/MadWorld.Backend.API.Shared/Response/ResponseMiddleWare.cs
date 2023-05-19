using System.Net;
using System.Text.Json;
using JetBrains.Annotations;
using LanguageExt.ClassInstances;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Shared.Contracts.Shared.Error;
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
        var errorResponse = new ErrorResponse();

        if (exception is ValidationException validationException)
        {
            newResponse.StatusCode = HttpStatusCode.BadRequest;
            errorResponse.Message = validationException.Message;
        }
        else
        {
            newResponse.StatusCode = HttpStatusCode.InternalServerError;
            errorResponse.Message = "There went something wrong. Please try again later.";
        }
        
        await newResponse.WriteStringAsync(JsonSerializer.Serialize(errorResponse));
        context.GetInvocationResult().Value = newResponse;
    }
}