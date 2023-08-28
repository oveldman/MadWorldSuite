using System;
using MadWorld.Backend.Domain.Blogs;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace MadWorld.Backend.JobRunner.Functions.Blog;

public class BlogDeletedCleanup
{
    private readonly IBlogDeletedCleanupUseCase _useCase;

    public BlogDeletedCleanup(IBlogDeletedCleanupUseCase useCase)
    {
        _useCase = useCase;
    }
    
    [Function("BlogDeletedCleanup")]
    public void Run([TimerTrigger("0 0 4 * * SAT")] FunctionContext context)
    {
        _useCase.StartCleanUp();
    }
}