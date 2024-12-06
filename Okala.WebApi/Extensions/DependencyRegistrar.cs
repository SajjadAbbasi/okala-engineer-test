using Okala.WebApi.Mappings;
using Serilog;

namespace Okala.WebApi.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddWebApi(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(WebApiMappingProfile));
        return services;
    }
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .CreateLogger();

        builder.Host.UseSerilog();

    }

}