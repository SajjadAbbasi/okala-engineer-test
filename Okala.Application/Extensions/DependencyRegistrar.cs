using Microsoft.Extensions.DependencyInjection;
using Okala.Application.Interfaces.UseCases;
using Okala.Application.UseCases;

namespace Okala.Application.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IExchangeService, ExchangeService>();
        return services;
    }
}