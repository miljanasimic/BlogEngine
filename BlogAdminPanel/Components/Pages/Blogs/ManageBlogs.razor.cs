using BlogAPI.Features.Blogs.DTOs;
using BlogAdminPanel.Components.Services;
using Microsoft.AspNetCore.Components;
using BlogAdminPanel.Components.Shared;

namespace BlogAdminPanel.Components.Pages.Blogs
{
    public partial class ManageBlogs : ComponentBase
    {
        [Inject]
        public BlogService BlogService { get; set; } = default!;

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        protected IEnumerable<BlogItemDto> blogs = new List<BlogItemDto>();

        private bool isLoading = true;

        private ConfirmModal? deleteModal;
        private int blogIdToDelete;

        protected override async Task OnInitializedAsync()
        {
            isLoading = true;
            blogs = await BlogService.GetBlogsAsync();
            isLoading = false;
        }

        protected void OpenCreate()
        {
            Navigation.NavigateTo("/blogs/new");
        }

        protected void Edit(int id)
        {
            Navigation.NavigateTo($"/blogs/edit/{id}");
        }

        protected Task ConfirmDelete(int id)
        {
            blogIdToDelete = id;
            deleteModal?.Show();
            return Task.CompletedTask;
        }

        private async Task OnDeleteConfirmed(bool confirmed)
        {
            if (confirmed)
            {
                await BlogService.DeleteBlogAsync(blogIdToDelete);
                blogs = (await BlogService.GetBlogsAsync()).ToList();
            }
            blogIdToDelete = 0;
        }
    }
}