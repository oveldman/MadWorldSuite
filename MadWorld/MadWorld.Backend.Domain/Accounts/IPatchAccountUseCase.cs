using LanguageExt.Common;
using MadWorld.Shared.Contracts.Authorized.Account;

namespace MadWorld.Backend.Domain.Accounts;

public interface IPatchAccountUseCase
{
    Task<Result<PatchAccountResponse>> PatchAccount(PatchAccountRequest? request);
}