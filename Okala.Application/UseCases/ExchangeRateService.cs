using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Application.Interfaces.UseCases;

namespace Okala.Application.UseCases;

public class ExchangeRateService(IExchangeAggregatorExternalService exchangeExternalService): IExchangeRateService
{
    public async Task<IEnumerable<ExchangeRate>> GetExchangeRateByCode(string baseCurrencyCode)
    {
        var targetCurrenciesList = new [] { "USD","EUR","BRL","GBP","AUD" };
        var exchangeRates = await exchangeExternalService.GetRateByCurrencyCode(baseCurrencyCode, targetCurrenciesList);
        return exchangeRates;
    }
}