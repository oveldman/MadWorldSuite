using System.Net;
using System.Reflection;
using System.Text.Json;
using JetBrains.Annotations;
using LanguageExt;
using LanguageExt.Common;
using LanguageExt.UnsafeValueAccess;
using MadWorld.Backend.API.Shared.Functions.Expansions;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Shared.Contracts.Shared.Error;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace MadWorld.Backend.API.Shared.Response;

[UsedImplicitly]
public class ResponseMiddleWare : IFunctionsWorkerMiddleware
{
    private readonly IFunctionContextWrapper _functionContextWrapper;

    public ResponseMiddleWare(IFunctionContextWrapper functionContextWrapper)
    {
        _functionContextWrapper = functionContextWrapper;
    }
    
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        await next(context);

        var resultProcessed = _functionContextWrapper.GetInvocationResult(context).Value;
        
        if (IsParentType(resultProcessed, typeof(Result<>)))
        {
            UnwrapResultProcessed(context, nameof(UnwrapResult), resultProcessed);
        }
        
        if (IsParentType(resultProcessed, typeof(Option<>)))
        {
            UnwrapResultProcessed(context, nameof(UnwrapOption), resultProcessed);
        }
    }

    private static bool IsParentType<T>(T value, Type parentType)
    {
        var valueType = value?.GetType();
        return valueType is { IsGenericType: true } &&
               valueType.GetGenericTypeDefinition().IsAssignableFrom(parentType);
    }

    private void UnwrapResultProcessed<T>(FunctionContext context, string unwrapMethodName, T resultProcessed)
    {
        var resultType = resultProcessed!.GetType();
        var valueType = resultType.GetGenericArguments().FirstOrDefault();
        var unwrapMethod = GetType().GetMethod(unwrapMethodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
        var genericUnwrapMethod = unwrapMethod?.MakeGenericMethod(valueType!);
        genericUnwrapMethod?.Invoke(unwrapMethod!.IsStatic ? null : this, new object[] { context, resultProcessed });
    }

    private void UnwrapResult<T>(FunctionContext context, Result<T> result)
    {
        result.IfSucc(value =>
        {
            if (IsParentType(value, typeof(Option<>)))
            {
                UnwrapResultProcessed(context, nameof(UnwrapOption), value);
            }
            else
            {
                UnwrapValue(context, value);   
            }
        });
        result.IfFail(exception => UnwrapException(context, exception).GetAwaiter().GetResult());
    }

    private async Task UnwrapOption<T>(FunctionContext context, Option<T> option)
    {
        if (option.IsSome)
        {
            UnwrapValue(context, option.ValueUnsafe());
            return;
        }
        
        var errorResponse = new ErrorResponse
        {
            Message = "Not found"
        };

        await SetResponse(context, errorResponse, HttpStatusCode.NotFound);
    }

    private void UnwrapValue<T>(FunctionContext context, T value)
    {
        _functionContextWrapper.GetInvocationResult(context).Value = value;
    }
    
    private async Task UnwrapException(FunctionContext context, Exception exception)
    {
        if (exception is ValidationException validationException)
        {
            var errorResponse = new ErrorResponse
            {
                Message = validationException.Message
            };
            await SetResponse(context, errorResponse, HttpStatusCode.BadRequest);
        }
        else
        {
            var errorResponse = new ErrorResponse
            {
                Message = "There went something wrong. Please try again later."
            };
            await SetResponse(context, errorResponse, HttpStatusCode.InternalServerError);
        }
    }

    private async Task SetResponse<T>(FunctionContext context, T response, HttpStatusCode statusCode)
    {
        var request = await context.GetHttpRequestDataAsync();
        var newResponse = request!.CreateResponse();
        newResponse.StatusCode = statusCode;
        await newResponse.WriteStringAsync(JsonSerializer.Serialize(response));
        _functionContextWrapper.GetInvocationResult(context).Value = newResponse;
    }
}