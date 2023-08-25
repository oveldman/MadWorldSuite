using LanguageExt;
using LanguageExt.Common;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Frontend.Domain.Accounts;

public interface IAccountService
{
    Task<Option<GetAccountResponse>> GetAccountAsync(string id);
    Task<GetAccountsResponse> GetAccountsAsync();
    Task<Result<OkResponse>> PatchAccountAsync(PatchAccountRequest request);
}