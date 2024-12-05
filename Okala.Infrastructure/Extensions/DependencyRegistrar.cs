using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Infrastructure.ConnectedServices.Exchange;
using Okala.Infrastructure.Mappings;
using Refit;

namespace Okala.Infrastructure.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        services.AddRefitClient<ICoinMarketCapClient>()
            .ConfigureHttpClient(c =>
            {
                var apiUrl = Environment.GetEnvironmentVariable("EXCHANGE_API_URL")?? throw new NullReferenceException();
                var apiToken = Environment.GetEnvironmentVariable("EXCHANGE_API_TOKEN")?? throw new NullReferenceException();
                c.BaseAddress = new Uri(apiUrl);
                c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY",apiToken);
            });
        services.AddScoped<IExchangeExternalService, CoinMarketCapService>();
        services.AddScoped<IExchangeAggregatorExternalService, ExchangeAggregatorService>();
        
        services.AddAutoMapper(typeof(InfrastructureMappingProfile));
        return services;
    }
}