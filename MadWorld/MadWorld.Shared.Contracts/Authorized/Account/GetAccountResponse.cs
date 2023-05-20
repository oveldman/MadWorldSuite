using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Shared.Contracts.Authorized.Account;

public class GetAccountResponse : IResponse
{
    public AccountContract Account { get; set; } = new();
}