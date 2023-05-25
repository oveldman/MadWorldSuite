using JetBrains.Annotations;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Shared.Contracts.Shared.Authorization;

namespace MadWorld.Backend.Api.Shared.Unittests.Authorization.TestUtils;

[UsedImplicitly]
public class MockFunction
{
    [UsedImplicitly]
    [Authorize(RoleTypes.Admin)]
    public void Run()
    {
        
    }
}