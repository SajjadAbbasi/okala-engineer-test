using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Application.Interfaces.Persistence;
using Okala.Application.Interfaces.UseCases;

namespace Okala.Application.UseCases;

public class ExchangeService(
    IExchangeRepository exchangeRepository,
    IExchangeAggregatorClient exchangeClient) : IExchangeService
{
    public async Task<IList<ExchangeRate>> GetExchangeRateByCode(string baseCurrencyCode)
    {
        var fiatCurrencyList = exchangeRepository.GetFiatCurrencyList().Select(c => c.Code).ToArray();
        var exchangeRates = await exchangeClient.GetRateByCurrencyCode(baseCurrencyCode, fiatCurrencyList);
        return exchangeRates;
    }
}