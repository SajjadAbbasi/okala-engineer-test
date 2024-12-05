using System.Text.Json.Serialization;

namespace Okala.Infrastructure.ConnectedServices.Exchange.DTOs;

public record ExchangeRateResponse(
    [property: JsonPropertyName("status")] Status Status,
    [property: JsonPropertyName("data")] IDictionary<string, IEnumerable<Currency>> Data);

public record Status(
    [property: JsonPropertyName("timestamp")] DateTime Timestamp,
    [property: JsonPropertyName("error_code")] int ErrorCode,
    [property: JsonPropertyName("error_message")] string? ErrorMessage);

public record Currency(
    [property: JsonPropertyName("id")] long Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("symbol")] string Code,
    [property: JsonPropertyName("slug")] string Slug,
    [property: JsonPropertyName("quote")] IDictionary<string, Quote> Quote);

public record Quote(
    [property: JsonPropertyName("price")] decimal Price,
    [property: JsonPropertyName("last_updated")] DateTime LastUpdate);