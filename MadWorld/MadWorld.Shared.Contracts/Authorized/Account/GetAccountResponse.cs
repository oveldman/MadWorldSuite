using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Authorized.Account;

public sealed class GetAccountResponse : IResponse
{
    public AccountDetailContract Account { get; set; } = new();
}