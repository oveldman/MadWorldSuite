using System.Collections.Generic;
using System.Net;
using MadWorld.Backend.API.Shared.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.API.Authorized.Functions.Test;

public static class PingWithUsername
{
    [Function("PingWithUsername")]
    public static string Run([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req,
        FunctionContext executionContext)
    {
        var user = executionContext.GetUser();
        
        return $"Authorized: Welcome to Azure Functions, {user.Name}!!";
    }
}