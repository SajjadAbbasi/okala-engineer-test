using Okala.Application.DTOs.ConnectedServices.ExchangeService;

namespace Okala.Application.Interfaces.ConnectedServices;

public interface IExchangeAggregatorClient
{
    public Task<IList<ExchangeRate>>  GetRateByCurrencyCode(string baseCurrencyCode,string[] targetCurrenciesCode);
}