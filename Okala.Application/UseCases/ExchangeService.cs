using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Application.Interfaces.Persistence;
using Okala.Application.Interfaces.UseCases;

namespace Okala.Application.UseCases;

public class ExchangeService(
    IExchangeRepository exchangeRepository,
    IExchangeAggregatorExternalService exchangeExternalService) : IExchangeService
{
    public async Task<IEnumerable<ExchangeRate>> GetExchangeRateByCode(string baseCurrencyCode)
    {
        var fiatCurrencyList = exchangeRepository.GetFiatCurrencyList().Select(c => c.Code).ToArray();
        var exchangeRates = await exchangeExternalService.GetRateByCurrencyCode(baseCurrencyCode, fiatCurrencyList);
        return exchangeRates;
    }
}