using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;

namespace Okala.Infrastructure.ConnectedServices.Exchange;

public class CoinMarketCapService(ICoinMarketCapClient client) : IExchangeExternalService
{
    public async Task<ExchangeRate> GetRateByCurrencyCode(string baseCurrencyCode, string targetCurrencyCode)
    {
        var response = await client.GetQuotes(baseCurrencyCode, targetCurrencyCode);
        var baseCurrency=response.Data[baseCurrencyCode];
        return new ExchangeRate(new Currency(1,"BTC","USD"),
            new Currency(1,"BTC","USD"),
            10500);
    }
}