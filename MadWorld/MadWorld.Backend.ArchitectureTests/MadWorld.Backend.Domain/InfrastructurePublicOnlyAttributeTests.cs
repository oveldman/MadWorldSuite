using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using LanguageExt.ClassInstances;
using MadWorld.Backend.Domain.System;

namespace MadWorld.Backend.ArchitectureTests.MadWorld.Backend.Domain;

public class InfrastructurePublicOnlyAttributeTests
{
    [Fact]
    public void Methods_With_RepositoryPublicOnlyAttribute_Only_Called_By_Repositories()
    {
        var rule =
            Types()
                .That()
                .ResideInNamespace($"{AssemblyMarkers.DomainNamespace}.*", true)
                .Should()
                .FollowCustomCondition(HasOnlyRepositoryDependencyWithAttribute, 
                    "This methode is only called by repository",
                    "This methode should only be used by the repository");

        rule.Check(AssemblyMarkers.Architecture);
    }

    private static bool HasOnlyRepositoryDependencyWithAttribute(IType type)
    {
        var members = type.Members
            .Where(m => m.AttributeInstances.Any(attribute => attribute.Type.Name == nameof(RepositoryPublicOnlyAttribute)))
            .ToList();

        return members.All(member =>
        {
            return member.BackwardsDependencies
                .All(
                    dependency => (dependency.Origin.Namespace.Name.StartsWith(AssemblyMarkers.InfrastructureNamespace) && 
                                  dependency.Origin.Name.EndsWith("Repository")) || 
                                  member.FullName.Contains(dependency.Origin.FullName));
        });
    }
}