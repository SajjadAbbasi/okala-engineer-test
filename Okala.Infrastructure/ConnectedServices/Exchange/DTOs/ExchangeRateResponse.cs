using Newtonsoft.Json;

namespace Okala.Infrastructure.ConnectedServices.Exchange.DTOs;

public record ExchangeRateResponse(
    [property: JsonProperty("status")] Status Status,
    [property: JsonProperty("data")] IDictionary<string, IList<Currency>> Data);

public record Status(
    [property: JsonProperty("timestamp")] DateTime Timestamp,
    [property: JsonProperty("error_code")] int ErrorCode,
    [property: JsonProperty("error_message")] string? ErrorMessage);

public record Currency(
    [property: JsonProperty("id")] long Id,
    [property: JsonProperty("name")] string Name,
    [property: JsonProperty("symbol")] string Code,
    [property: JsonProperty("slug")] string Slug,
    [property: JsonProperty("quote")] IDictionary<string, IList<Quote>> Quote);

public record Quote(
    [property: JsonProperty("price")] decimal Price,
    [property: JsonProperty("last_updated")] DateTime LastUpdate);