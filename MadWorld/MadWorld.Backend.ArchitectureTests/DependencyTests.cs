namespace MadWorld.Backend.ArchitectureTests;

public class DependencyTests
{
    [Fact]
    public void ApplicationDependsNotOnInfrastructure()
    {
        var rule = Types().That().ResideInNamespace($"{AssemblyMarkers.ApplicationNamespace}.*", true)
            .Should().NotDependOnAny($"{AssemblyMarkers.InfrastructureNamespace}.*", true);

        rule.Check(AssemblyMarkers.Architecture);
    }
    
    [Fact]
    public void DomainDependsNotOnBackendProjects()
    {
        var backendProjects = new List<string>()
        {
            $"{AssemblyMarkers.AnonymousNamespace}.*",
            $"{AssemblyMarkers.AuthorizedNamespace}.*",
            $"{AssemblyMarkers.SharedNamespace}.*",
            $"{AssemblyMarkers.ApplicationNamespace}.*",
            $"{AssemblyMarkers.InfrastructureNamespace}.*",
        };

        var rule = Types().That().ResideInNamespace($"{AssemblyMarkers.DomainNamespace}.*", true)
            .Should().NotDependOnAny(backendProjects, true);

        rule.Check(AssemblyMarkers.Architecture);
    }
}