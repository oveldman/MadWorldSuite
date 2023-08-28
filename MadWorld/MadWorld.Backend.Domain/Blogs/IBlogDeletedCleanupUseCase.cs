namespace MadWorld.Backend.Domain.Blogs;

public interface IBlogDeletedCleanupUseCase
{
    Task StartCleanUp();
}