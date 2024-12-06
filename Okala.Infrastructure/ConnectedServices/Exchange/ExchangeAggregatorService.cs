using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;

namespace Okala.Infrastructure.ConnectedServices.Exchange;

public class ExchangeAggregatorService(IExchangeClient exchangeService)
    : IExchangeAggregatorClient
{
    public async Task<IEnumerable<ExchangeRate>> GetRateByCurrencyCode(string baseCurrencyCode, string[] targetCurrenciesCode)
    {
        var tasks= targetCurrenciesCode.Select(code => 
            exchangeService.GetRateByCurrencyCode(baseCurrencyCode, code));
        var exchangeRates =await Task.WhenAll(tasks);
        var exchangeRatesFlatList= exchangeRates.SelectMany(e=>e);
        return exchangeRatesFlatList;
    }
}