using System.Net.Http;
using System.Text.Json;
using AnimeVerse.Models;
using AnimeVerse.Factories;

namespace AnimeVerse.Services
{
    public class AnimeService : IAnimeService
    {
        private readonly HttpClient _httpClient;
        // HttpClient injiceras via Dependency Injection. På detta sätt återanvänds samma instans och onödiga resurser undviks.

        public AnimeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Anime>> GetPopularAnimeAsync()
        {
            // Asynkront API-anrop till Jikan för att hämta populära animer.
            var response = await _httpClient.GetAsync("https://api.jikan.moe/v4/top/anime");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var data = doc.RootElement.GetProperty("data");

            // Här skapas en lista med Anime-objekt via Factory-metoden.
            var result = new List<Anime>();
            foreach (var item in data.EnumerateArray().Take(10))
            {
                result.Add(AnimeFactory.CreateFromJsonElement(item));
            }

            return result;
        }

        public async Task<List<Anime>> SearchAnimeAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return new List<Anime>();

            var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/anime?q={query}&limit=10");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var data = doc.RootElement.GetProperty("data");

            // Här itererar det igenom resultatet och använder Factory-klassen för att skapa objekt.
            var result = new List<Anime>();
            foreach (var item in data.EnumerateArray())
            {
                result.Add(AnimeFactory.CreateFromJsonElement(item));
            }

            return result;
        }

        public async Task<Anime?> GetAnimeByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"https://api.jikan.moe/v4/anime/{id}");
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);
            var item = doc.RootElement.GetProperty("data");
            
            // Här skapas ett enskilt anime objekt från JSON med hjälp av Factory-metoden.
            return AnimeFactory.CreateFromJsonElement(item);
        }
    }
}
