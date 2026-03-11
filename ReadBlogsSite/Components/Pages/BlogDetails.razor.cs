
using BlogAPI.Features.Blogs.DTOs;
using Microsoft.AspNetCore.Components;
using ReadBlogsSite.Components.Services;
using Markdig;
namespace ReadBlogsSite.Components.Pages
{
    public partial class BlogDetails : ComponentBase
  {

    [Inject]
    public BlogService BlogService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
     
    [Parameter]
    public int Id { get; set; } = 0;

    private BlogDetailsDto blogModel = new()
    {
        AuthorId = 0,
        Title = "",
        ShortDescription = "",
        Content = "",
        Language = "",
        Tags = []
    };

    private MarkupString RenderedContent => new(Markdown.ToHtml(blogModel.Content ?? ""));

    protected override async Task OnInitializedAsync()
    {
        blogModel = await BlogService.GetBlogByIdAsync(Id);
    }

    private void BackToBlogsHomePage() => Navigation.NavigateTo("/");
  }
}