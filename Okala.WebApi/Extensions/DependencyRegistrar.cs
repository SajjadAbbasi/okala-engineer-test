using Okala.WebApi.Mappings;

namespace Okala.WebApi.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(WebApiMappingProfile));
        return services;
    }
}