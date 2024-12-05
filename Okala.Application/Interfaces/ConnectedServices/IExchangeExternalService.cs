using Okala.Application.DTOs.ConnectedServices.ExchangeService;

namespace Okala.Application.Interfaces.ConnectedServices;

public interface IExchangeExternalService
{
    public Task<ExchangeRate> GetRateByCurrencyCode(string baseCurrencyCode,string targetCurrencyCode);
}