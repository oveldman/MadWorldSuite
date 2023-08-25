using LanguageExt.Common;
using MadWorld.Shared.Contracts.Authorized.Account;
using MadWorld.Shared.Contracts.Shared.Functions;

namespace MadWorld.Backend.Domain.Accounts;

public interface IPatchAccountUseCase
{
    Result<OkResponse> PatchAccount(PatchAccountRequest? request);
}