using JetBrains.Annotations;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using Microsoft.AspNetCore.Components;

namespace MadWorld.Frontend.UI.Admin.Pages.Blogs;

[UsedImplicitly]
public partial class BlogDetails
{
    [Parameter]
    public string Id { get; set; } = null!;
    private BlogDetailContract Blog { get; set; } = new();

    private bool IsNewBlog => string.IsNullOrWhiteSpace(Id);
    private bool IsReady { get; set; }

    [Inject]
    private IGetBlogUseCase GetBlogUseCase { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        if (!IsNewBlog)
        {
            Blog = await GetBlogUseCase.GetBlog(Id);
        }
        
        IsReady = true;
        await base.OnInitializedAsync();
    }
}