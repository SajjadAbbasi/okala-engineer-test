using Okala.Application.Interfaces.Persistence;
using Okala.Core.Entities;

namespace Okala.Infrastructure.Persistence.Repositories;

public class ExchangeMockRepository : IExchangeRepository
{
    public IQueryable<AvailableCurrency> GetFiatCurrencyList()
    {
        var currenciesCodeList = new [] { "USD","EUR","BRL","GBP","AUD" };
        long id = 1;
        var currenciesList= currenciesCodeList.Select(code =>
            new AvailableCurrency(id++, code));
        return currenciesList.AsQueryable();
    }
}