using System;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.JobRunner.FunctionAddons;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.JobRunner.Functions.Blog;

public class BlogDeletedCleanup
{
    private const string Schedule = "0 0 4 * * SAT";
    
    private readonly IBlogDeletedCleanupUseCase _useCase;

    public BlogDeletedCleanup(IBlogDeletedCleanupUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Function("BlogDeletedCleanup")]
    public async Task Run([TimerTrigger(Schedule)] FunctionContext context)
    {
        await _useCase.StartCleanUp();
    }
}