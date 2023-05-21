using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace MadWorld.Backend.API.Shared.Functions.Expansions;

public class FunctionContextWrapper : IFunctionContextWrapper
{
    public async Task<HttpRequestData?> GetHttpRequestDataAsync(FunctionContext context)
    {
        return await context.GetHttpRequestDataAsync();
    }
    
    public InvocationResult GetInvocationResult(FunctionContext context)
    {
        return context.GetInvocationResult();
    }
}