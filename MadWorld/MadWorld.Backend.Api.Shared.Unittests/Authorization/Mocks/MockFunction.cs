using JetBrains.Annotations;
using MadWorld.Backend.API.Shared.Authorization;
using MadWorld.Shared.Contracts.Shared.Authorization;

namespace MadWorld.Backend.Api.Shared.Unittests.Authorization.Mocks;

[UsedImplicitly]
[Authorize(RoleTypes.Admin)]
public class MockFunction
{
    
}