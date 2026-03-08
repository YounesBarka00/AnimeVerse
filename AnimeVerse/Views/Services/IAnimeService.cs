using AnimeVerse.Models;

namespace AnimeVerse.Services
{
    public interface IAnimeService
    {
        Task<List<Anime>> GetPopularAnimeAsync();
        Task<List<Anime>> SearchAnimeAsync(string query);
        Task<Anime?> GetAnimeByIdAsync(int id);
    }
}