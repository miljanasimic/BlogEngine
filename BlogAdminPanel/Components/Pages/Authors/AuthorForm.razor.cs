using BlogAdminPanel.Components.Services;
using BlogAPI.Features.Authors.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlogAdminPanel.Components.Pages.Authors
{
    public partial class AuthorForm : ComponentBase
  {

    [Inject]
    public AuthorService AuthorService { get; set; } = default!;

    [Inject]
    public NavigationManager Navigation { get; set; } = default!;
     
    [Parameter]
    public int Id { get; set; } = 0;

    private AuthorDto authorModel = new()
    {
        Id = 0,
        FirstName = string.Empty,
        LastName = string.Empty,
    };

    private EditContext editContext;

    protected override async Task OnInitializedAsync()
    {
        editContext = new EditContext(authorModel);

        if (Id != 0)
        {
            authorModel = await AuthorService.GetAuthorByIdAsync(Id);
        }

         editContext = new EditContext(authorModel);
    }

    private bool IsValid() => editContext.Validate();

    private async Task SaveAuthor()
    {
        if (Id == 0)
            await AuthorService.CreateAuthorAsync(authorModel);
        else
            await AuthorService.UpdateAuthorAsync(authorModel);

        Navigation.NavigateTo("/authors");
    }

    private void Cancel() => Navigation.NavigateTo("/authors");
  }
}