using System.Text.Json;
using AnimeVerse.Factories;

namespace AnimeVerse.Tests
{
    public class AnimeFactoryTests
    {
        [Fact]
        public void Create_ShouldReturnAnimeObject()
        {
            var json = "{\"mal_id\":1,\"title\":\"Naruto\",\"score\":8.5,\"year\":2002,\"images\":{\"jpg\":{\"image_url\":\"test.jpg\"}},\"synopsis\":\"En ung ninja som vill bli Hokage.\"}";
            var element = JsonDocument.Parse(json).RootElement;
            
            var anime = AnimeFactory.CreateFromJsonElement(element);
            
            Assert.NotNull(anime);
            Assert.Equal("Naruto", anime.Title);
            Assert.Equal(8.5, anime.Score);
            Assert.Equal(2002, anime.Year);
        }

        [Fact]
        public void Create_ShouldHandleMissingFieldsGracefully()
        {
            // Här testas hur AnimeFactory hanterar ofullständig data. Om JSON saknar vissa fält ska koden fortfarande skapa ett giltigt objekt.
            var json = "{\"title\":\"Okänd\"}";
            var element = JsonDocument.Parse(json).RootElement;
            
            var anime = AnimeFactory.CreateFromJsonElement(element);
            
            Assert.NotNull(anime);
            Assert.Equal("Okänd", anime.Title);
            Assert.True(anime.Score < 0 || anime.Score == 0);
        }

    }
}