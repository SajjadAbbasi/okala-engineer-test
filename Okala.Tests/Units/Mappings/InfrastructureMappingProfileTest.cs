using AutoMapper;
using FluentAssertions;
using Okala.Infrastructure.ConnectedServices.Exchange.DTOs;
using Okala.Infrastructure.Mappings;
using Xunit;
using AppCurrency = Okala.Application.DTOs.ConnectedServices.ExchangeService.Currency;
using AppExchangeRate = Okala.Application.DTOs.ConnectedServices.ExchangeService.ExchangeRate;

namespace Okala.Tests.Units.Mappings;

public class InfrastructureMappingProfileTest
{
    private readonly IMapper _actual;
    public InfrastructureMappingProfileTest()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<InfrastructureMappingProfile>();
        });

        _actual = configuration.CreateMapper();
    }

    [Fact]
    public void Currency_is_mapped_to_ExchangeRate_successfully()
    {
        //arrange
        var currency1 = new Currency(1, "Bitcoin", "BTC", "bitcoin", 
            new Dictionary<string, Quote>
            {
                {"USD",new Quote(98500.0124m,new DateTime(2024,10,10))},
            });
        var currency2 = new Currency(2, "Tron", "TRN", "tron", 
            new Dictionary<string, Quote>
            {
                {"USD",new Quote(10200.050m,new DateTime(2024,10,20))},
            });
        var listOfCurrencies = new[] { currency1, currency2 };
        //act
        var results = _actual.Map<IEnumerable<AppExchangeRate>>(listOfCurrencies);
        //assert
        var exchangeRates = results.ToList();
        exchangeRates.Should().HaveCount(2);
        exchangeRates.Should().Contain(new AppExchangeRate(
            new AppCurrency(1,"Bitcoin","BTC"), "USD",98500.0124m ));
        exchangeRates.Should().Contain(new AppExchangeRate(
            new AppCurrency(2,"Tron","TRN"), "USD",10200.050m ));
    }
}