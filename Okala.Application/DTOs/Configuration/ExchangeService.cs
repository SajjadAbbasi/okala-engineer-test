namespace Okala.Application.DTOs.Configuration;

public record ExchangeServiceConfig
{
    public string ServiceName { get; set; }
    public string BaseUrl { get; set; }
    public string ApiToken { get; set; }
}