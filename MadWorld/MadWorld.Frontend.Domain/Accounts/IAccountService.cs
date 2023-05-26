using LanguageExt;
using LanguageExt.Common;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Frontend.Domain.Accounts;

public interface IAccountService
{
    Task<Option<GetAccountResponse>> GetAccountAsync(string id);
    Task<GetAccountsResponse> GetAccountsAsync();
    Task<Result<PatchAccountResponse>> PatchAccountAsync(PatchAccountRequest request);
}