using Okala.Infrastructure.ConnectedServices.Exchange.DTOs;
using Refit;

namespace Okala.Infrastructure.ConnectedServices.Exchange;

public interface ICoinMarketCapClient
{
    [Get("/v2/cryptocurrency/quotes/latest")]
    Task<ExchangeRateResponse> GetQuotes(
        [AliasAs("symbol")] string baseCurrencyCode,
        [AliasAs("convert")] string targetCurrencyCode);
}