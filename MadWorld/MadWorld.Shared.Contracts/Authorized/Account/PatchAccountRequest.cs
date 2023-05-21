namespace MadWorld.Shared.Contracts.Authorized.Account;

public class PatchAccountRequest
{
    public string Id { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}