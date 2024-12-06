using System.Security.Authentication;

namespace Okala.Infrastructure.Middlewares;

public class HttpSSL2Handler :HttpClientHandler
{
    public HttpSSL2Handler()
    {
        SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13;
    }
}