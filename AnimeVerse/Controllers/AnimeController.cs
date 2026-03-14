using AnimeVerse.Models;
using AnimeVerse.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeVerse.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeService _animeService;

        public AnimeController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                var popularAnime = await _animeService.GetPopularAnimeAsync();
                return View("Anime", popularAnime ?? new List<Anime>());
            }

            var animeResults = await _animeService.SearchAnimeAsync(search);

            if (animeResults == null || !animeResults.Any())
            {
                ViewBag.Message = $"No anime found for '{search}'. Please check the spelling and try again.";
                return View("Anime", new List<Anime>());
            }

            return View("Anime", animeResults);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Retrieves a single anime by its ID and returns a 404 page if it doesn't exist
            var anime = await _animeService.GetAnimeByIdAsync(id);

            if (anime == null)
                return NotFound();

            return View(anime);
        }
    }
}