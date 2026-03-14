using System.Text.Json;
using AnimeVerse.Models;
using AnimeVerse.Factories;

namespace AnimeVerse.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly HttpClient _httpClient;

        public AnimeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Anime>> GetPopularAnimeAsync()
        {
            var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/top/anime");
            
            // Ensures the API request succeeded; otherwise an exception is thrown
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var data = doc.RootElement.GetProperty("data");

            return data
                .EnumerateArray()
                .Take(10)
                .Select(AnimeFactory.CreateFromJsonElement)
                .ToList();
        }

        public async Task<List<Anime>> SearchAnimeAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Anime>();

            var encodedQuery = Uri.EscapeDataString(query);

            var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/anime?q={encodedQuery}&limit=10");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var data = doc.RootElement.GetProperty("data");

            return data
                .EnumerateArray()
                
                // Convert each JSON element from the API into an Anime object using the factory
                .Select(AnimeFactory.CreateFromJsonElement)
                .ToList();
        }

        public async Task<Anime?> GetAnimeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/anime/{id}");

            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(json);
            var item = doc.RootElement.GetProperty("data");

            return AnimeFactory.CreateFromJsonElement(item);
        }
    }
}
