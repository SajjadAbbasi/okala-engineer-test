using Okala.Application.DTOs.ConnectedServices.ExchangeService;

namespace Okala.Application.Interfaces.ConnectedServices;

public interface IExchangeAggregatorExternalService
{
    public Task<IEnumerable<ExchangeRate>>  GetRateByCurrencyCode(string baseCurrencyCode,string[] targetCurrenciesCode);
}