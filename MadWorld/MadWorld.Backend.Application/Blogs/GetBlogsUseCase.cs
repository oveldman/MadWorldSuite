using LanguageExt.Common;
using MadWorld.Backend.Domain.Blogs;
using MadWorld.Backend.Domain.Exceptions;
using MadWorld.Shared.Contracts.Anonymous.Blog;

namespace MadWorld.Backend.Application.Blogs;

public class GetBlogsUseCase : IGetBlogsUseCase
{
    private const int MinimumPageNumber = 0;
    
    private readonly IBlogRepository _repository;
    
    public GetBlogsUseCase(IBlogRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<GetBlogsResponse>> GetBlogsAsync(GetBlogsRequest request)
    {
        if (!int.TryParse(request.Page, out var pageNumber))
        {
            return new Result<GetBlogsResponse>(new ValidationException($"{nameof(request.Page)} must be a number"));
        }
        
        if (pageNumber < MinimumPageNumber)
        {
            return new Result<GetBlogsResponse>(new ValidationException($"{nameof(request.Page)} must be a higher than {MinimumPageNumber}"));
        }
        
        var blogCount = await _repository.CountBlogs();
        var blogs = _repository.GetBlogs(pageNumber);
        var blogContracts = blogs.Select(b => b.ToContract()).ToList();
        return new GetBlogsResponse(blogCount, blogContracts);
    }
}