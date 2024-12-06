using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.Infrastructure.ConnectedServices.Exchange;
using Okala.Infrastructure.ConnectedServices.Exchange.DTOs;
using Xunit;
using Currency = Okala.Infrastructure.ConnectedServices.Exchange.DTOs.Currency;
using AppExchangeRate = Okala.Application.DTOs.ConnectedServices.ExchangeService.ExchangeRate;
using AppCurrency = Okala.Application.DTOs.ConnectedServices.ExchangeService.Currency;

namespace Okala.Tests.Units.ConnectedServices;

public class CoinMarketCapServiceTest
{
    private readonly ICoinMarketCapClient _client;
    private readonly IMapper _mapper;

    public CoinMarketCapServiceTest()
    {
        _client = Substitute.For<ICoinMarketCapClient>();
        _mapper = Substitute.For<IMapper>();
    }
    [Theory]
    [InlineData("BTC")]
    public async Task Exchange_rate_returned_by_currency_code_successfully(string baseCurrencyCode)
    {
        //arrange
        var listOfCurrencies = new[]
        {
            new Currency(1, "Bitcoin", "BTC", "bitcoin",
                new Dictionary<string, Quote>
                {
                    { "USD", new Quote(98500.0124m, new DateTime(2024, 10, 10)) },
                }),
        };
        var listOfExchangeRates = new[]
        {
            new AppExchangeRate(
                new AppCurrency(1, "Bitcoin", "BTC"), "USD", 98500.0124m),
        };
        _mapper.Map<IList<ExchangeRate>>(listOfCurrencies).ReturnsForAnyArgs(listOfExchangeRates);
        
        var clientResult = Task.FromResult(
            new ExchangeRateResponse(new Status(DateTime.Now, 0, null),
                new Dictionary<string, IEnumerable<Currency>>
                {
                    {
                        "BTC", new []
                        {
                            new Currency(1, "Bitcoin", "BTC", "bitcoin",
                                new Dictionary<string, Quote>
                                {
                                    { "USD", new Quote(98500.0124m, new DateTime(2024, 10, 20)) }
                                })
                        }
                    }
                })
        );
        _client.GetQuotes("BTC", "USD").Returns(clientResult);
        var actual = new CoinMarketCapService(_client,_mapper);
        //act
        var results =await actual.GetRateByCurrencyCode("BTC", "USD");
        //assert
        results.Should().HaveCount(1);
        results.Should().BeEquivalentTo(listOfExchangeRates);
    }
}