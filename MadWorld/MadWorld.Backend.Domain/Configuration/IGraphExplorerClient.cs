using LanguageExt;
using MadWorld.Backend.Domain.Accounts;

namespace MadWorld.Backend.Domain.Configuration;

public interface IGraphExplorerClient
{
    Task<Option<Account>> GetUserAsync(string id);
    Task<IReadOnlyList<Account>> GetUsersAsync();
}