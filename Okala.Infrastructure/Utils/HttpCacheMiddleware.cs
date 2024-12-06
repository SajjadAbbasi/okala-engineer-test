using Polly;

namespace Okala.Infrastructure.Utils;

public class HttpCacheMiddleware(IAsyncPolicy<string> cachePolicy) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Define a cache key based on the request URI
        var cacheKey = request.RequestUri?.ToString();

        // Use the Polly cache policy to handle the request
        var responseContent = await cachePolicy.ExecuteAsync(async context =>
        {
            var response = await base.SendAsync(request, cancellationToken);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }, new Context(cacheKey));

        // Create a new HttpResponseMessage from the cached content
        return new HttpResponseMessage
        {
            Content = new StringContent(responseContent),
            RequestMessage = request,
            StatusCode = System.Net.HttpStatusCode.OK
        };
    }
}