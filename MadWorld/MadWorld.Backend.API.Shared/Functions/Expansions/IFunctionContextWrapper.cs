using Microsoft.Azure.Functions.Worker;

namespace MadWorld.Backend.API.Shared.Functions.Expansions;

public interface IFunctionContextWrapper
{
    InvocationResult GetInvocationResult(FunctionContext context);
}