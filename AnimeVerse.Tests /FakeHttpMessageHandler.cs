using System.Net;

namespace AnimeVerse.Tests
{
    // Replaces real HTTP calls during testing by returning a predefined response instead of calling an external API
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly string _response;

        public FakeHttpMessageHandler(string response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Simulate different API outcomes: return 404 if the response contains "error", otherwise return 200 OK
            var statusCode = _response.Contains("error")
                ? HttpStatusCode.NotFound
                : HttpStatusCode.OK;

            var message = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(_response)
            };

            return Task.FromResult(message);
        }
    }
}