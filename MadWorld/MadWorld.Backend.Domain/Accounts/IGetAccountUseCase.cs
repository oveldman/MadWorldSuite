using LanguageExt;
using LanguageExt.Common;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Domain.Accounts;

public interface IGetAccountUseCase
{
    Result<Option<GetAccountResponse>> GetAccount(GetAccountRequest request);
}