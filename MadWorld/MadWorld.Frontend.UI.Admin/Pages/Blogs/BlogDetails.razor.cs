using JetBrains.Annotations;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Frontend.Domain.Blogs.Extensions;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using MadWorld.Shared.Contracts.Shared.Functions;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace MadWorld.Frontend.UI.Admin.Pages.Blogs;

[UsedImplicitly]
public partial class BlogDetails
{
    [Parameter]
    public string Id { get; set; } = null!;
    private BlogDetailContract Blog { get; set; } = new();
    private string BodyUtf8 { get; set; } = string.Empty;

    private bool IsNewBlog => string.IsNullOrWhiteSpace(Id);
    private bool HasError { get; set; }
    private bool IsReady { get; set; }
    private bool IsSaved { get; set; }

    [Inject]
    private DialogService DialogService { get; set; } = null!;
    
    [Inject]
    private IAddBlogUseCase AddBlogUseCase { get; set; } = null!;
    
    [Inject]
    private IDeleteBlogUseCase DeleteBlogUseCase { get; set; } = null!;
    
    [Inject]
    private IGetBlogUseCase GetBlogUseCase { get; set; } = null!;
    
    [Inject]
    private IUpdateBlogUseCase UpdateBlogUseCase { get; set; } = null!;
    
    [Inject] 
    private NavigationManager NavigationManager { get; set; } = null!;
    
    protected override async Task OnInitializedAsync()
    {
        if (!IsNewBlog)
        {
            Blog = await GetBlogUseCase.GetBlog(Id);
            BodyUtf8 = Blog.GetUtf8Body();
        }
        
        IsReady = true;
        await base.OnInitializedAsync();
    }

    private async Task SaveChanges(BlogDetailContract blog)
    {
        ResetMessages();

        if (IsNewBlog)
        {
            await AddBlog(blog);
        }
        else
        {
            await UpdateBlog(blog);
        }
    }

    private async Task AddBlog(BlogDetailContract blog)
    {
        var response = await AddBlogUseCase.AddBlog(blog, BodyUtf8);
        SetMessages(response);
    }
    
    public async Task ConfirmDelete(string id)
    {
        ResetMessages();
        
        var confirmationResult = await DialogService.Confirm($"Are you sure to delete blog {id}?", "Delete Blog", new ConfirmOptions
        {
            OkButtonText = "Yes", 
            CancelButtonText = "Cancel",
            CloseDialogOnEsc = true,
            CloseDialogOnOverlayClick = true
        });
        
        if (confirmationResult ?? false)
        {
            await DeleteBlog(id);
        }
    }

    private async Task DeleteBlog(string id)
    {
        var succeed = await DeleteBlogUseCase.DeleteBlog(id);

        if (succeed)
        {
            NavigationManager.NavigateTo($"/Blogs");
        }
        else
        {
            HasError = true;
        }
    }
    
    private async Task UpdateBlog(BlogDetailContract blog)
    {
        var response = await UpdateBlogUseCase.UpdateBlog(blog, BodyUtf8);
        SetMessages(response);
    }
    
    private void CancelChanges()
    {
        NavigationManager.NavigateTo($"/Blogs");
    }

    private void SetMessages(OkResponse okResponse)
    {
        IsSaved = okResponse.IsSuccess;
        HasError = !IsSaved;
    }

    private void ResetMessages()
    {
        IsSaved = false;
        HasError = false;
    }
}