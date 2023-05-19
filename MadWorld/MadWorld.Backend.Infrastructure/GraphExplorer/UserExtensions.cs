using Microsoft.Graph.Models;

namespace MadWorld.Backend.Infrastructure.GraphExplorer;

public static class UserExtensions
{
    public static bool HasFound(this User? user)
    {
        return user?.Id != null;
    }
}