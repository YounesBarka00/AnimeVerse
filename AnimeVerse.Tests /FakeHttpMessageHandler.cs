using System.Net;

namespace AnimeVerse.Tests
{
    // Denna klass ersätter verkliga HTTP-anrop under testning.
    // Den skickar tillbaka ett fördefinierat JSON svar istället för att anropa ett riktigt API.
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