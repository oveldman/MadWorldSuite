using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Authorized.Account;

public sealed class PatchAccountResponse : IResponse
{
    public bool Succeeded { get; private set; } = true;
}