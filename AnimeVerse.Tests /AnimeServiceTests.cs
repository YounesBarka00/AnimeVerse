using AnimeVerse.Services;

namespace AnimeVerse.Tests
{
    public class AnimeServiceTests
    {
        [Fact]
        public async Task GetPopularAnimeAsync_ShouldReturnListOfAnimes()
        {
            // Här testas AnimeService utan att göra riktiga API anrop genom att använda en fejkad HttpMessageHandler.
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
            // Här testar vi hur AnimeService reagerar när API:et returnerar ett fel (404 Not Found).
            var fakeHandler = new FakeHttpMessageHandler("{\"error\":\"Not Found\"}");
            var httpClient = new HttpClient(fakeHandler)
            {
                BaseAddress = new Uri("https://api.jikan.moe/v4/")
            };

            var service = new AnimeService(httpClient);

            // Act – anropa metoden med ett ogiltigt id
            var result = await service.GetAnimeByIdAsync(999999);

            // Assert – resultatet ska vara null eftersom API:et misslyckades (404)
            Assert.Null(result);
        }
    }
}