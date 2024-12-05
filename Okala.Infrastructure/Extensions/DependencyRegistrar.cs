using Microsoft.Extensions.DependencyInjection;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Infrastructure.ConnectedServices.Exchange;
using Refit;

namespace Okala.Infrastructure.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddRefitClient<ICoinMarketCapClient>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://pro-api.coinmarketcap.com");
                c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY","480487b0-5ce6-4007-a544-a98f4ea6228d");
            });
        services.AddScoped<IExchangeExternalService, CoinMarketCapService>();
        services.AddScoped<IExchangeAggregatorExternalService, ExchangeAggregatorService>();
        return services;
    }
}