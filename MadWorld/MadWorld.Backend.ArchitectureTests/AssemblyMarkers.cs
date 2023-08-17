using ArchUnitNET.Loader;
using MadWorld.Backend.API.Anonymous;
using MadWorld.Backend.API.Authorized;
using MadWorld.Backend.API.Shared;
using MadWorld.Backend.Application;
using MadWorld.Backend.Domain;
using MadWorld.Backend.Infrastructure;

namespace MadWorld.Backend.ArchitectureTests;

public static class AssemblyMarkers
{
    public static readonly ArchUnitNET.Domain.Architecture Architecture = new ArchLoader().LoadAssemblies(
            typeof(IDomainMarker).Assembly,
            typeof(IAnonymousMarker).Assembly,
            typeof(IAuthorizedMarker).Assembly,
            typeof(ISharedMarker).Assembly,
            typeof(IApplicationMarker).Assembly,
            typeof(IInfrastructureMarker).Assembly)
        .Build();

    public static readonly string DomainNamespace = typeof(IDomainMarker).Namespace!;
    public static readonly string AnonymousNamespace = typeof(IAnonymousMarker).Namespace!;
    public static readonly string AuthorizedNamespace = typeof(IAuthorizedMarker).Namespace!; 
    public static readonly string SharedNamespace = typeof(ISharedMarker).Namespace!;
    public static readonly string ApplicationNamespace = typeof(IApplicationMarker).Namespace!;
    public static readonly string InfrastructureNamespace = typeof(IInfrastructureMarker).Namespace!;
}