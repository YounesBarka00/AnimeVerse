using AnimeVerse.Models;
using AnimeVerse.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimeVerse.Controllers
{
    public class AnimeController : Controller
    {
        private readonly IAnimeService _animeService;
        
        // Dependency Injection av tjänsten som hanterar API-anropen.
        
        public AnimeController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        public async Task<IActionResult> Index(string? search)
        {
            // Här sker en asynkron hämtning av data så att sidan inte låser sig vid API-anrop.
            if (string.IsNullOrWhiteSpace(search))
            {
                var popularAnimes = await _animeService.GetPopularAnimeAsync();
                return View("Anime", popularAnimes);
            }
            
            var animes = await _animeService.SearchAnimeAsync(search);

            if (animes == null || !animes.Any())
            {
                ViewBag.Message = $"Ingen anime hittades för '{search}'. Kontrollera stavningen och försök igen.";
                return View("Anime", new List<Anime>());
            }

            return View("Anime", animes);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Här hämtas detaljerad information från API:t baserat på anime-ID.
            var anime = await _animeService.GetAnimeByIdAsync(id);
            if (anime == null)
                return NotFound();

            return View(anime);
        }
    }
}