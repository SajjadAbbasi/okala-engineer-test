using Okala.Application.DTOs.Configuration;
using Okala.WebApi.Mappings;
using Serilog;

namespace Okala.WebApi.Extensions;

public static class DependencyRegistrar
{
    public static IServiceCollection AddWebApi(this IServiceCollection service,WebApplicationBuilder builder)
    {
        service.AddAutoMapper(typeof(WebApiMappingProfile));
        service.AddLogger(builder);
        return service;
    }
    public static IServiceCollection AddConfigs(this IServiceCollection service,WebApplicationBuilder builder)
    {
        builder.Services.Configure<ExchangeServiceConfig>(cfg=>
        {
            var section = builder.Configuration.GetSection("ExchangeService");
            cfg.ServiceName = section.GetSection("ServiceName").Get<string>()??
                              throw new NullReferenceException();
            cfg.BaseUrl = section.GetSection("BaseUrl").Get<string>()??
                          throw new NullReferenceException();
            cfg.ApiToken = Environment.GetEnvironmentVariable("EXCHANGE_API_TOKEN") ??
                           throw new NullReferenceException();
        });
        
        
        return service;
    }
    
    private static void AddLogger(this IServiceCollection service,WebApplicationBuilder builder)
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