using BlogAPI.Features.Blogs.DTOs;
using ReadBlogsSite.Components.Services;
using Microsoft.AspNetCore.Components;

namespace ReadBlogsSite.Components.Pages
{
    public partial class BlogsHomePage : ComponentBase
    {
        [Inject]
        public BlogService BlogService { get; set; } = default!;

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        protected IEnumerable<BlogItemDto> blogs = [];

        protected IEnumerable<string> availableLanguages = [];
        protected IEnumerable<TagDto> availableTags = [];

        [Parameter]
        [SupplyParameterFromQuery(Name = "tag")]
        public string? SelectedTag { get; set; }

        [Parameter]
        [SupplyParameterFromQuery(Name = "lang")]
        public string? SelectedLang { get; set; }

        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            // isLoading = true;
            availableLanguages = await BlogService.GetLanguagesAsync();
            availableTags = await BlogService.GetTagsAsync();
            // blogs = await BlogService.GetBlogsAsync(tag: Tag, language: Lang);
            // isLoading = false;
        }

        protected override async Task OnParametersSetAsync()
        {
            isLoading = true;
            blogs = await BlogService.GetBlogsAsync(tag: SelectedTag, language: SelectedLang);
            isLoading = false;
        }

        protected void OpenBlogDetails(int id)
        {
            Navigation.NavigateTo($"/blogs/{id}");
        }

        private string formatUrlWithFilters(string? tag, string? lang)
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(tag))
                queryParams.Add($"tag={tag}");
            if (!string.IsNullOrEmpty(lang))
                queryParams.Add($"lang={lang}");

            return "/blogs" + (queryParams.Count > 0 ? "?" + string.Join("&", queryParams) : "");
        }

        void OnTagFilterChanged(ChangeEventArgs e)
        {
            var newTag = e.Value?.ToString();

            if (newTag == SelectedTag)
                return;

            SelectedTag = newTag;
            Navigation.NavigateTo(formatUrlWithFilters(SelectedTag, SelectedLang));
        }

        void OnLanguageFilterChanged(ChangeEventArgs e)
        {
            var newLang = e.Value?.ToString();

            if (newLang == SelectedLang)
                return;
            SelectedLang = newLang;
            Navigation.NavigateTo(formatUrlWithFilters(SelectedTag, SelectedLang));
        }
    }
}