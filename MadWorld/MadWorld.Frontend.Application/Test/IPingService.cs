namespace MadWorld.Frontend.Application.Test;

public interface IPingService
{
    Task<string>  GetAnonymousAsync();
    Task<string>  GetAuthorizedAsync();
}