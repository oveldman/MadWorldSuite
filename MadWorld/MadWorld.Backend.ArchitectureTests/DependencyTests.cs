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

namespace MadWorld.Backend.ArchitectureTests;

public class DependencyTests
{
    private static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(
            typeof(IDomainMarker).Assembly,
            typeof(IAnonymousMarker).Assembly,
            typeof(IAuthorizedMarker).Assembly,
            typeof(ISharedMarker).Assembly,
            typeof(IApplicationMarker).Assembly,
            typeof(IInfrastructureMarker).Assembly)
        .Build();

    private static readonly string DomainNamespace = typeof(IDomainMarker).Namespace!;
    private static readonly string AnonymousNamespace = typeof(IAnonymousMarker).Namespace!;
    private static readonly string AuthorizedNamespace = typeof(IAuthorizedMarker).Namespace!; 
    private static readonly string SharedNamespace = typeof(ISharedMarker).Namespace!;
    private static readonly string ApplicationNamespace = typeof(IApplicationMarker).Namespace!;
    private static readonly string InfrastructureNamespace = typeof(IInfrastructureMarker).Namespace!;

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