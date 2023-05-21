using LanguageExt;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.General;

namespace MadWorld.Backend.Domain.Configuration;

public interface IGraphExplorerClient
{
    Task<Option<Account>> GetUserAsync(GuidId id);
    Task<IReadOnlyList<Account>> GetUsersAsync();
}