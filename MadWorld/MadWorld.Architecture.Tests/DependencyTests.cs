using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using MadWorld.Backend.API.Anonymous;
using MadWorld.Backend.API.Authorized;
using MadWorld.Backend.API.Shared;
using MadWorld.Backend.Application;
using MadWorld.Backend.Domain;
using MadWorld.Backend.Infrastructure;

//add a using directive to ArchUnitNET.Fluent.ArchRuleDefinition to easily define ArchRules
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace MadWorld.Architecture.Tests;

public class DependencyTests
{
    private static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(
            typeof(DomainMarker).Assembly,
            typeof(AnonymousMarker).Assembly,
            typeof(AuthorizedMarker).Assembly,
            typeof(SharedMarker).Assembly,
            typeof(ApplicationMarker).Assembly,
            typeof(InfrastructureMarker).Assembly)
        .Build();

    private static readonly string DomainNamespace = typeof(DomainMarker).Namespace!;
    private static readonly string AnonymousNamespace = typeof(AnonymousMarker).Namespace!;
    private static readonly string AuthorizedNamespace = typeof(AuthorizedMarker).Namespace!; 
    private static readonly string SharedNamespace = typeof(SharedMarker).Namespace!;
    private static readonly string ApplicationNamespace = typeof(ApplicationMarker).Namespace!;
    private static readonly string InfrastructureNamespace = typeof(InfrastructureMarker).Namespace!;

    [Fact]
    public void ApplicationDependsNotOnInfrastructure()
    {
        var rule = Types().That().ResideInNamespace($"{ApplicationNamespace}.*", true)
            .Should().NotDependOnAny($"{InfrastructureNamespace}.*", true);

        rule.Check(Architecture);
    }
    
    [Fact]
    public void DomainDependsNotOnBackendProject()
    {
        var backendProjects = new List<string>()
        {
            $"{AnonymousNamespace}.*",
            $"{AuthorizedNamespace}.*",
            $"{SharedNamespace}.*",
            $"{ApplicationNamespace}.*",
            $"{InfrastructureNamespace}.*",
        };

        var rule = Types().That().ResideInNamespace($"{DomainNamespace}.*", true)
            .Should().NotDependOnAny(backendProjects, true);

        rule.Check(Architecture);
    }
}