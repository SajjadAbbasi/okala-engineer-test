using Polly;

namespace Okala.Infrastructure.Middlewares;

public class HttpCacheMiddleware(IAsyncPolicy<string> cachePolicy) : DelegatingHandler
{
    private const string CacheKeyPrefix = "F6AC3384-B011-49FF-B1C5-8021E3A95A22";
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        // Define a cache key based on the request URI
        var cacheKey = $"{CacheKeyPrefix}-{request.RequestUri?.AbsoluteUri}";

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