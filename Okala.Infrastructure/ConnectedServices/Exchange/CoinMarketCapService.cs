using AutoMapper;
using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;

namespace Okala.Infrastructure.ConnectedServices.Exchange;

public class CoinMarketCapService(ICoinMarketCapClient apiClient,IMapper mapper) : IExchangeClient
{
    public async Task<IList<ExchangeRate>> GetRateByCurrencyCode(string baseCurrencyCode, string targetCurrencyCode)
    {
        var response = await apiClient.GetQuotes(baseCurrencyCode, targetCurrencyCode);
        response.Data.TryGetValue(baseCurrencyCode, out var currencyList);
        var exchangeRates = mapper.Map<IList<ExchangeRate>>(currencyList ??  []);
        return exchangeRates;
    }
}