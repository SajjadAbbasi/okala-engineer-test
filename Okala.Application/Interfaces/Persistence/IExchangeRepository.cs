using Okala.Core.Entities;

namespace Okala.Application.Interfaces.Persistence;

public interface IExchangeRepository
{
    public IList<AvailableCurrency> GetFiatCurrencyList();
}