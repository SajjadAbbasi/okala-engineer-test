using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Okala.Application.DTOs.Configuration;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Application.Interfaces.Persistence;
using Okala.Infrastructure.ConnectedServices.Exchange;
using Okala.Infrastructure.Mappings;
using Okala.Infrastructure.Middlewares;
using Okala.Infrastructure.Persistence.Repositories;
using Polly;
using Polly.Caching.Memory;
using Polly.Extensions.Http;
using Refit;

namespace Okala.Infrastructure.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddCoinMarketCapClient();
        services.AddScoped<IExchangeExternalService, CoinMarketCapService>();
        services.AddScoped<IExchangeAggregatorExternalService, ExchangeAggregatorService>();
        
        services.AddScoped<IExchangeRepository, ExchangeMockRepository>();
        
        services.AddAutoMapper(typeof(InfrastructureMappingProfile));
        return services;
    }

    private static void AddCoinMarketCapClient(this IServiceCollection services)
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
        services.AddTransient<HttpExceptionHandler>();

        services.AddRefitClient<ICoinMarketCapClient>()
            .ConfigureHttpClient(c =>
            {
                using var scope = services.BuildServiceProvider().CreateScope();
                var config= scope.ServiceProvider.GetRequiredService<IOptions<ExchangeServiceConfig>>();
                c.BaseAddress = new Uri(config.Value.BaseUrl);
                c.DefaultRequestHeaders.Add("X-CMC_PRO_API_KEY", config.Value.ApiToken);
            }).ConfigurePrimaryHttpMessageHandler<HttpSSL2Handler>()
            .AddHttpMessageHandler<HttpCacheMiddleware>()
            .AddHttpMessageHandler<HttpExceptionHandler>()
            .AddPolicyHandler(retryPolicy);
    }
}