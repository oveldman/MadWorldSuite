using JetBrains.Annotations;
using MadWorld.Frontend.Domain.Blogs;
using MadWorld.Shared.Contracts.Anonymous.Blog;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace MadWorld.Frontend.UI.Admin.Pages.Blogs;

[UsedImplicitly]
public partial class BlogOverview
{
   private string pagingSummaryFormat = "Displaying page {0} of {1} <b>(total {2} records)</b>";
   private RadzenDataGrid<BlogContract> dataGrid;
   
   private int PagePosition = 0;
   
   private bool _isReady;
   private bool _isLoading => !_isReady;
   
   private int _totalRecords = 0;
   private IReadOnlyCollection<BlogContract> _blogs = Array.Empty<BlogContract>();

   [Inject]
   private IGetBlogsUseCase GetBlogsUseCase { get; set; } = null!;
   
   [Inject] 
   private NavigationManager NavigationManager { get; set; } = null!;

   protected override async Task OnInitializedAsync()
   {
      await LoadBlogs();
      
      _isReady = true;
      await base.OnInitializedAsync();
   }
   
   private async Task LoadData(LoadDataArgs args)
   {
      var skip = args.Skip ?? 0;
      var top = args.Top ?? 0;

      PagePosition = skip / top;
      
      await LoadBlogs();
   }

   private async Task LoadBlogs()
   {
      var result = await GetBlogsUseCase.GetBlogsAsync(PagePosition);
      
      _blogs = result.Blogs;
      _totalRecords = result.Count;
   }
   
   private void OpenBlogDetails(DataGridRowMouseEventArgs<BlogContract> blog)
   {
      NavigationManager.NavigateTo($"/Blog/{blog.Data.Id}");
   }
   
   private void OpenNewBlog()
   {
      NavigationManager.NavigateTo($"/Blog");
   }
}