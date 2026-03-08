using System.Text.Json;
using AnimeVerse.Models;

namespace AnimeVerse.Factories
{
    public static class AnimeFactory
    {
        public static Anime CreateFromJsonElement(JsonElement item)
        {
            double score = -1;
            int year = -1;
            string imageUrl = "";
            string synopsis = "";

            // Här kontrolleras bland annat att egenskapen "score" finns och att den verkligen är ett nummer.
            
            if (item.TryGetProperty("score", out var scoreEl) && scoreEl.ValueKind == JsonValueKind.Number)
                score = scoreEl.GetDouble();

            if (item.TryGetProperty("year", out var yearEl) && yearEl.ValueKind == JsonValueKind.Number)
                year = yearEl.GetInt32();

            if (item.TryGetProperty("images", out var imgEl) &&
                imgEl.TryGetProperty("jpg", out var jpgEl) &&
                jpgEl.TryGetProperty("image_url", out var urlEl))
                imageUrl = urlEl.GetString() ?? "";

            if (item.TryGetProperty("synopsis", out var synEl) && synEl.ValueKind == JsonValueKind.String)
                synopsis = synEl.GetString() ?? "";

            // Här skapas själva Anime-objektet och alla fält tilldelas.
            // Varje egenskap kontrolleras så att programmet inte kraschar om något saknas i API-datat.
            return new Anime
            {
                MalId = item.TryGetProperty("mal_id", out var idEl) && idEl.ValueKind == JsonValueKind.Number ? idEl.GetInt32() : 0,
                Title = item.TryGetProperty("title", out var titleEl) && titleEl.ValueKind == JsonValueKind.String ? titleEl.GetString() ?? "Okänd" : "Okänd",
                Score = score,
                Year = year,
                ImageUrl = imageUrl,
                Synopsis = string.IsNullOrWhiteSpace(synopsis) ? "Ingen beskrivning tillgänglig." : synopsis
            };
        }
    }
}