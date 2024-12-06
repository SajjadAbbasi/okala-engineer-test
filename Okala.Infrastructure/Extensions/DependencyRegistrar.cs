using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Application.Interfaces.Persistence;
using Okala.Infrastructure.ConnectedServices.Exchange;
using Okala.Infrastructure.Mappings;
using Okala.Infrastructure.Persistence.Repositories;
using Okala.Infrastructure.Utils;
using Polly;
using Polly.Caching.Memory;
using Polly.Extensions.Http;
using Refit;

namespace Okala.Infrastructure.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.ConfigureCoinMarketCapClient();
        services.AddScoped<IExchangeExternalService, CoinMarketCapService>();
        services.AddScoped<IExchangeAggregatorExternalService, ExchangeAggregatorService>();
        
        services.AddScoped<IExchangeRepository, ExchangeMockRepository>();
        
        services.AddAutoMapper(typeof(InfrastructureMappingProfile));
        return services;
    }

    private static void ConfigureCoinMarketCapClient(this IServiceCollection services)
    {
        services.AddMemoryCache();
        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        
        services.AddTransient<HttpCacheMiddleware>(provider =>
        {
            var memoryCache = provider.GetRequiredService<IMemoryCache>();
            var cacheProvider = new MemoryCacheProvider(memoryCache);
            var cachePolicy = Policy.CacheAsync<string>(cacheProvider, TimeSpan.FromMinutes(1));
            return new HttpCacheMiddleware(cachePolicy);
        });
        services.AddTransient<HttpSSL2Handler>();
        services.AddRefitClient<ICoinMarketCapClient>()
            .ConfigureHttpClient(c =>
            {
                var apiUrl = Environment.GetEnvironmentVariable("EXCHANGE_API_URL") ??
                             throw new NullReferenceException();
                var apiToken = Environment.GetEnvironmentVariable("EXCHANGE_API_TOKEN") ??
                               throw new NullReferenceException();
                c.BaseAddress = new Uri(apiUrl);
                c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", apiToken);
            }).ConfigurePrimaryHttpMessageHandler<HttpSSL2Handler>()
            .AddHttpMessageHandler<HttpCacheMiddleware>()
            .AddPolicyHandler(retryPolicy);
    }
}