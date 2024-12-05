using AutoMapper;
using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;

namespace Okala.Infrastructure.ConnectedServices.Exchange;

public class CoinMarketCapService(ICoinMarketCapClient client,IMapper mapper) : IExchangeExternalService
{
    public async Task<IEnumerable<ExchangeRate>> GetRateByCurrencyCode(string baseCurrencyCode, string targetCurrencyCode)
    {
        var response = await client.GetQuotes(baseCurrencyCode, targetCurrencyCode);
        var currencyList=response.Data[baseCurrencyCode];
        var exchangeRates = mapper.Map<IEnumerable<ExchangeRate>>(currencyList);
        return exchangeRates;
    }
}