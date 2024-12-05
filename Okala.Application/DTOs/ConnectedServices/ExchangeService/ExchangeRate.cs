namespace Okala.Application.DTOs.ConnectedServices.ExchangeService;

public record ExchangeRate(
    Currency BaseCurrency,
    string TargetCurrencyCode,
    decimal Price
    );
public record Currency(
    long Id,
    string Name,
    string Code
);