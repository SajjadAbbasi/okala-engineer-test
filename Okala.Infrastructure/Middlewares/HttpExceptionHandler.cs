namespace Okala.Infrastructure.Middlewares;

public class HttpExceptionHandler: DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException($"Error: {response.StatusCode}, Content: {errorContent}");
        }

        return response;
    }
}