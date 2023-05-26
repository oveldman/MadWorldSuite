using MadWorld.Frontend.Domain.General;

namespace MadWorld.Frontend.Domain.Accounts;

public interface IPatchAccountUseCase
{
    Task<PatchResult> PatchAccount(Account account);
}