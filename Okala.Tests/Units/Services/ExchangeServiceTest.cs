using FluentAssertions;
using NSubstitute;
using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Application.Interfaces.ConnectedServices;
using Okala.Application.Interfaces.Persistence;
using Okala.Application.UseCases;
using Okala.Core.Entities;
using Xunit;

namespace Okala.Tests.Units.Services;

public class ExchangeServiceTest
{
    private readonly IExchangeRepository _exchangeRepository;
    private readonly IExchangeAggregatorClient _exchangeClient;

    public ExchangeServiceTest()
    {
        _exchangeRepository = Substitute.For<IExchangeRepository>();
        _exchangeClient = Substitute.For<IExchangeAggregatorClient>();
    }
    
    [Theory]
    [InlineData("BTC")]
    public async Task Common_fiat_exchange_rates_returned_successfully(string baseCurrencyCode)
    {
        //arrange
        var availableCurrencies = new List<AvailableCurrency>
        {
            new (1,"USD"), new (2,"EUR"), new (3,"BRL"), new (4,"GBP"),
            new (5,"AUD"),
        };
        var exchangeRates = availableCurrencies.Select(ac =>
            new ExchangeRate(new Currency(1, "Bitcoin", "BTC"), ac.Code, 1200.0022m)).ToList();
        _exchangeRepository.GetFiatCurrencyList().Returns(availableCurrencies);
        _exchangeClient.GetRateByCurrencyCode(baseCurrencyCode,availableCurrencies.Select(ac=>ac.Code)
            .ToArray()).Returns(exchangeRates);
        
        var actual = new ExchangeService(_exchangeRepository, _exchangeClient);
        
        //act
        var result = await actual.GetExchangeRateByCode(baseCurrencyCode);
        
        
        //assert
        result.Should().BeSubsetOf(exchangeRates);
    }
    
}