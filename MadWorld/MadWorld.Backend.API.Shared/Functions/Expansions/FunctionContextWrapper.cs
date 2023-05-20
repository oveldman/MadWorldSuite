using Microsoft.Azure.Functions.Worker;

namespace MadWorld.Backend.API.Shared.Functions.Expansions;

public class FunctionContextWrapper : IFunctionContextWrapper
{
    public InvocationResult GetInvocationResult(FunctionContext context)
    {
        return context.GetInvocationResult();
    }
}