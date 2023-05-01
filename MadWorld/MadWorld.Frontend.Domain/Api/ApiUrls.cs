namespace MadWorld.Frontend.Domain.Api;

public class ApiUrls
{
    public const string SectionName = "ApiUrls";
    
    public string Anonymous { get; init; } = string.Empty;
    public string Authorized { get; init; } = string.Empty;
    public string BaseUrlAuthorized { get; init; } = string.Empty;
}