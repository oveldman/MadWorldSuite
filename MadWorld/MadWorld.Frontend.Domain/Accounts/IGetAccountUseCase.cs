using LanguageExt;

namespace MadWorld.Frontend.Domain.Accounts;

public interface IGetAccountUseCase
{
    Task<Option<Account>> GetAccountAsync(string id);
}