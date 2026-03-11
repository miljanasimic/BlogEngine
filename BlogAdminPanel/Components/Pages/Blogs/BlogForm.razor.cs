using BlogAdminPanel.Components.Services;
using BlogAPI.Features.Authors.DTOs;
using BlogAPI.Features.Blogs.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlogAdminPanel.Components.Pages.Blogs
{
    public partial class BlogForm : ComponentBase
  {

    [Inject]
    public BlogService BlogService { get; set; } = default!;

    [Inject]
    public AuthorService AuthorService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
     
    [Parameter]
    public int Id { get; set; } = 0;

    private BlogDetailsDto blogModel = new()
    {
        AuthorId = null,
        Title = "",
        ShortDescription = "",
        Content = "",
        Language = "",
        DatePublished = DateTime.Now

    };
    private string TagsText = "";
    private IEnumerable<AuthorDto> authors = [];

    private EditContext editContext;

    protected override async Task OnInitializedAsync()
    {
        authors = (await AuthorService.GetAuthorsAsync()).ToList();

        if (Id != 0)
        {
            blogModel = await BlogService.GetBlogByIdAsync(Id);
            TagsText = blogModel.Tags != null ? string.Join(", ", blogModel.Tags.Select(t => t.Name)) : "";
        }

         editContext = new EditContext(blogModel);
    }

    private bool IsValid() => editContext.Validate();

    private async Task SaveBlog()
    {
        var tags = TagsText.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).ToList();
        
        if (Id == 0)
            await BlogService.CreateBlogAsync(new CreateBlogDto
            {
                Title = blogModel.Title,
                ShortDescription = blogModel.ShortDescription,
                Content = blogModel.Content,
                Language = blogModel.Language,
                AuthorId = blogModel.AuthorId.Value,
                Tags = tags,
                DatePublished = blogModel.DatePublished
            });
        else
            await BlogService.UpdateBlogAsync(new UpdateBlogDto
            {
                Id = blogModel.Id,
                Title = blogModel.Title,
                ShortDescription = blogModel.ShortDescription,
                Content = blogModel.Content,
                Language = blogModel.Language,
                AuthorId = blogModel.AuthorId.Value,
                Tags = tags,
                DatePublished = blogModel.DatePublished

            });

        Navigation.NavigateTo("/blogs");
    }

    private void Cancel() => Navigation.NavigateTo("/blogs");
  }
}