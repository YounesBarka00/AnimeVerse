using System.Text.Json;
using AnimeVerse.Models;

namespace AnimeVerse.Factories
{
    public static class AnimeFactory
    {
        public static Anime CreateFromJsonElement(JsonElement item)
        {
            var malId = item.TryGetProperty("mal_id", out var idEl) && idEl.ValueKind == JsonValueKind.Number
                ? idEl.GetInt32()
                : 0;

            var title = item.TryGetProperty("title", out var titleEl) && titleEl.ValueKind == JsonValueKind.String
                ? titleEl.GetString() ?? "Unknown"
                : "Unknown";

            var score = item.TryGetProperty("score", out var scoreEl) && scoreEl.ValueKind == JsonValueKind.Number
                ? scoreEl.GetDouble()
                : -1;

            var year = item.TryGetProperty("year", out var yearEl) && yearEl.ValueKind == JsonValueKind.Number
                ? yearEl.GetInt32()
                : -1;

            var imageUrl = item.TryGetProperty("images", out var imgEl) &&
                           imgEl.TryGetProperty("jpg", out var jpgEl) &&
                           jpgEl.TryGetProperty("image_url", out var urlEl)
                ? urlEl.GetString() ?? ""
                : "";

            var synopsis = item.TryGetProperty("synopsis", out var synEl) && synEl.ValueKind == JsonValueKind.String
                ? synEl.GetString() ?? ""
                : "";

            if (string.IsNullOrWhiteSpace(synopsis))
                synopsis = "No description available.";

            return new Anime
            {
                MalId = malId,
                Title = title,
                Score = score,
                Year = year,
                ImageUrl = imageUrl,
                Synopsis = synopsis
            };
        }
    }
}