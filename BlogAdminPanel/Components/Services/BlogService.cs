using BlogAPI.Features.Blogs.DTOs;

namespace BlogAdminPanel.Components.Services
{
    public class BlogService
  {
        private readonly HttpClient _httpClient;

        public BlogService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BlogAPI");
        }

        public async Task<IEnumerable<BlogItemDto>> GetBlogsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<BlogItemDto>>("api/Blogs");
            return response!;
        }

        public async Task<BlogDetailsDto> GetBlogByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<BlogDetailsDto>($"api/Blogs/{id}");
            return response!;
        }

        public async Task CreateBlogAsync(CreateBlogDto blog)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Blogs", blog);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateBlogAsync(UpdateBlogDto blog)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Blogs/{blog.Id}", blog);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteBlogAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Blogs/{id}");
        }
  }
}