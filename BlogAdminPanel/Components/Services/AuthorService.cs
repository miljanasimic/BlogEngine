using BlogAPI.Features.Authors.DTOs;

namespace BlogAdminPanel.Components.Services
{
    public class AuthorService
  {
      private readonly HttpClient _httpClient;

        public AuthorService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("BlogAPI");
        }

        public async Task<IEnumerable<AuthorDto>> GetAuthorsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<AuthorDto>>("api/Authors");
            return response!;
        }


        public async Task<AuthorDto> GetAuthorByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<AuthorDto>($"api/Authors/{id}");
            return response!;
        }

        public async Task CreateAuthorAsync(AuthorDto author)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Authors", author);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateAuthorAsync(AuthorDto author)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Authors/{author.Id}", author);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            await _httpClient.DeleteAsync($"api/Authors/{id}");
        }
  }
}