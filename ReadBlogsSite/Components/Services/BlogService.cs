using BlogAPI.Features.Blogs.DTOs;

namespace ReadBlogsSite.Components.Services
{
    public class BlogService
  {
        private readonly HttpClient _httpClient;

        public BlogService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BlogAPI");
        }

        public async Task<IEnumerable<BlogItemDto>> GetBlogsAsync(string? tag = null, string? language = null)
        {
            var queryParams = new List<string>();
            if (!string.IsNullOrEmpty(tag))
                queryParams.Add($"tag={Uri.EscapeDataString(tag)}");

            if (!string.IsNullOrEmpty(language))
                queryParams.Add($"language={Uri.EscapeDataString(language)}");

            var url = "api/Blogs";
            if (queryParams.Any())
                url += "?" + string.Join("&", queryParams);

            var response = await _httpClient.GetFromJsonAsync<List<BlogItemDto>>(url);
            return response!;
        }

        public async Task<BlogDetailsDto> GetBlogByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<BlogDetailsDto>($"api/Blogs/{id}");
            return response!;
        }

        public async Task<IEnumerable<string>> GetLanguagesAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<string>>("api/Blogs/languages");
            return response!;
        }

        public async Task<IEnumerable<TagDto>> GetTagsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TagDto>>("api/Blogs/tags");
            return response!;
        }

  }
}