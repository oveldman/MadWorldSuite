using LanguageExt;
using MadWorld.Backend.Domain.Accounts;
using MadWorld.Backend.Domain.Configuration;
using MadWorld.Shared.Contracts.Shared.Authorization;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace MadWorld.Backend.Infrastructure.GraphExplorer;

public class GraphExplorerClient : IGraphExplorerClient
{
    private readonly GraphServiceClient _graphServiceClient;
    private readonly string _extensionApplicationId;
    private string RoleName => $"extension_{_extensionApplicationId}_Roles";

    internal GraphExplorerClient(GraphServiceClient graphServiceClient, GraphExplorerConfigurations configurations)
    {
        _graphServiceClient = graphServiceClient;
        _extensionApplicationId = configurations.ApplicationId.Replace("-", "");
    }
    
    public async Task<Option<Account>> GetUserAsync(string id)
    {
        var userResponse = await _graphServiceClient
            .Users[id]
            .GetAsync(request =>
            {
                request.QueryParameters.Select = new string[] { "Id", "DisplayName", RoleName, "mailNickname" };
            });

        return userResponse.HasFound() ? CreateAccount(userResponse!) : Option<Account>.None;
    }
    
    public async Task<IReadOnlyList<Account>> GetUsersAsync()
    {
        var usersResponse = await _graphServiceClient
                                    .Users
                                    .GetAsync(request =>
                                    {
                                        request.QueryParameters.Select = new string[] { "Id", "DisplayName", "mailNickname" };
                                    });
        
        return usersResponse?.Value?
            .Select(CreateAccount)
            .ToList() ?? new List<Account>();
    }
    
    private Account CreateAccount(User user)
    {
        user.AdditionalData.TryGetValue(RoleName, out var roles);
        
        return new Account()
        {
            Id = user.Id ?? string.Empty,
            Name = user.DisplayName ?? string.Empty,
            Roles = roles?.ToString() ?? RoleTypes.None.ToString(),
            IsResourceOwner = user.MailNickname?.Contains("#EXT#") ?? false
        };
    }
}