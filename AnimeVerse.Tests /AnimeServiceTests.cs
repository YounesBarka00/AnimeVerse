using AnimeVerse.Services;

namespace AnimeVerse.Tests
{
    public class AnimeServiceTests
    {
        [Fact]
        public async Task GetPopularAnimeAsync_ShouldReturnListOfAnimes()
        {
            // Simulate an API response using a fake HttpMessageHandler so the test does not depend on the real Jikan API
            var fakeJson = "{\"data\":[{\"mal_id\":1,\"title\":\"Naruto\",\"score\":8.1,\"year\":2002,\"images\":{\"jpg\":{\"image_url\":\"test.jpg\"}},\"synopsis\":\"Test\"}]}";

            var httpClient = new HttpClient(new FakeHttpMessageHandler(fakeJson));
            var service = new AnimeService(httpClient);
            
            var result = await service.GetPopularAnimeAsync();
            
            Assert.NotEmpty(result);
            Assert.Equal("Naruto", result[0].Title);
            Assert.Equal(8.1, result[0].Score);
        }
        
        [Fact]
        public async Task GetAnimeByIdAsync_ShouldReturnNull_WhenApiFails()
        {
            // Verify that the service returns null when the external API responds with an error (e.g., 404 Not Found)
            var fakeHandler = new FakeHttpMessageHandler("{\"error\":\"Not Found\"}");
            var httpClient = new HttpClient(fakeHandler)
            {
                BaseAddress = new Uri("https://api.jikan.moe/v4/")
            };

            var service = new AnimeService(httpClient);

            var result = await service.GetAnimeByIdAsync(999999);

            Assert.Null(result);
        }
    }
}