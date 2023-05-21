using LanguageExt;
using LanguageExt.Common;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Backend.Domain.General;
using MadWorld.Backend.Domain.LanguageExt;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Models.ODataErrors;

namespace MadWorld.Backend.Infrastructure.GraphExplorer;

public class GraphExplorerClient : IGraphExplorerClient
{
    private readonly GraphServiceClient _graphServiceClient;
    private readonly string _extensionApplicationId;
    private string RoleName => $"extension_{_extensionApplicationId}_Roles";

    private readonly ILogger<GraphExplorerClient> _logger;

    internal GraphExplorerClient(GraphServiceClient graphServiceClient, GraphExplorerConfigurations configurations, ILogger<GraphExplorerClient> logger)
    {
        _graphServiceClient = graphServiceClient;
        _logger = logger;
        _extensionApplicationId = configurations.ApplicationId.Replace("-", "");
    }
    
    public async Task<Option<Account>> GetUserAsync(GuidId id)
    {
        try
        {
            var userResponse = await _graphServiceClient
                .Users[id]
                .GetAsync(request =>
                {
                    request.QueryParameters.Select = new [] { "Id", "DisplayName", RoleName, "mailNickname" };
                });

            return userResponse.HasFound() ? CreateAccount(userResponse!) : Option<Account>.None;
        }
        catch (ODataError exception)
        {
            _logger.LogInformation(exception, "User with {UserId} not found", id.ToString());
            return Option<Account>.None;
        }
    }
    
    public async Task<IReadOnlyList<Account>> GetUsersAsync()
    {
        var usersResponse = await _graphServiceClient
                                    .Users
                                    .GetAsync(request =>
                                    {
                                        request.QueryParameters.Select = new [] { "Id", "DisplayName", "mailNickname" };
                                    });
        
        return usersResponse?.Value?
            .Select(CreateAccount)
            .ToList() ?? new List<Account>();
    }

    public async Task<Result<bool>> UpdateUser(Account account)
    {
        var user = CreateUser(account);

        try
        {
            await _graphServiceClient.Users[account.Id].PatchAsync(user);
        }
        catch (ODataError exception)
        {
            _logger.LogInformation(exception, "Couldn't update user {UserId}", account.Id.ToString());
            return new Result<bool>(exception);
        }
        
        return true;
    }
    
    private Account CreateAccount(User user)
    {
        user.AdditionalData.TryGetValue(RoleName, out var roles);
        var id = user.Id ?? string.Empty;
        var name = user.DisplayName ?? string.Empty;
        var rolesParsed = roles?.ToString() ?? RoleTypes.None.ToString();
        var isResourceOwner = user.MailNickname?.Contains("#EXT#") ?? false;
        
        return Account.Parse(id, name, rolesParsed, isResourceOwner).GetValue();
    }

    private User CreateUser(Account account)
    {
        return new User()
        {
            AdditionalData = new Dictionary<string, object>()
            {
                { RoleName, account.Roles }
            }
        };
    }
}