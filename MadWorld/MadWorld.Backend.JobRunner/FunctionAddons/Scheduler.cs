using JetBrains.Annotations;

namespace MadWorld.Backend.JobRunner.FunctionAddons;

public static class Scheduler
{
    [UsedImplicitly]
    public const string EveryMinute = "0 */1 * * * *";
}