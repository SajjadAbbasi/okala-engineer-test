namespace Okala.Application.DTOs.ConnectedServices.ExchangeService;

public record ExchangeRate(
    Currency BaseCurrency,
    Currency ExchangedCurrency,
    decimal Price
    );
public record Currency(
    long Id,
    string Name,
    string Code
);