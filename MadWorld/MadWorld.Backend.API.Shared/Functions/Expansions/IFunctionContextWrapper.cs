using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MadWorld.Backend.API.Shared.Functions.Expansions;

public interface IFunctionContextWrapper
{
    Task<HttpRequestData?> GetHttpRequestDataAsync(FunctionContext context);
    InvocationResult GetInvocationResult(FunctionContext context);
}