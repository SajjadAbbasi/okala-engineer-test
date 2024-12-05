namespace Okala.WebApi.Models;

public record ExchangeRateModel(
    CurrencyModel BaseCurrency,
    IEnumerable<PriceModel> TargetCurrencyPrice
    );
public record CurrencyModel(
    string Name,
    string Code
);
public record PriceModel(
    string Code,
    decimal Price
);