using Okala.Application.DTOs.ConnectedServices.ExchangeService;

namespace Okala.Application.Interfaces.UseCases;

public interface IExchangeRateService
{
    public Task<IEnumerable<ExchangeRate>> GetExchangeRateByCode(string baseCurrencyCode);
}