using BlogAdminPanel.Components.Services;
using Microsoft.AspNetCore.Components;
using BlogAdminPanel.Components.Shared;
using BlogAPI.Features.Authors.DTOs;

namespace BlogAdminPanel.Components.Pages.Authors
{
    public partial class ManageAuthors : ComponentBase
    {
        [Inject]
        public AuthorService AuthorService { get; set; } = default!;

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        protected IEnumerable<AuthorDto> authors = new List<AuthorDto>();

        private bool isLoading = true;

        private ConfirmModal? deleteModal;
        private int authorIdToDelete;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            authors = await AuthorService.GetAuthorsAsync();
            isLoading = false;
        }

        protected void OpenCreate()
        {
            Navigation.NavigateTo("/authors/new");
        }

        protected void Edit(int id)
        {
            Navigation.NavigateTo($"/authors/edit/{id}");
        }

        protected Task ConfirmDelete(int id)
        {
            authorIdToDelete = id;
            deleteModal?.Show();
            return Task.CompletedTask;
        }

        private async Task OnDeleteConfirmed(bool confirmed)
        {
            if (confirmed)
            {
                await AuthorService.DeleteAuthorAsync(authorIdToDelete);
                authors = (await AuthorService.GetAuthorsAsync()).ToList();
            }
            authorIdToDelete = 0;
        }
    }
}