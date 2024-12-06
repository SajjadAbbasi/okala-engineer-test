using AutoMapper;
using FluentAssertions;
using Okala.Application.DTOs.ConnectedServices.ExchangeService;
using Okala.WebApi.Mappings;
using Okala.WebApi.Models;
using Xunit;

namespace Okala.Tests.Units.Mappings;

public class WebApiMappingProfileTest
{
    private readonly IMapper _actual;
    public WebApiMappingProfileTest()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WebApiMappingProfile>();
        });

        _actual = configuration.CreateMapper();
    }

    [Fact]
    public void ExchangeRate_is_mapped_to_ExchangeRateModel_successfully()
    {
        //arrange
        var exchangeRate1 = new ExchangeRate(new Currency(1, "Bitcoin", "BTC"), "USD", 98000.1010m);
        var exchangeRate2 = new ExchangeRate(new Currency(1, "Bitcoin", "BTC"), "EUR", 90500.11m);
        var exchangeRateList =new [] {exchangeRate1, exchangeRate2};
        //act
        var results = _actual.Map<IEnumerable<ExchangeRateModel>>(exchangeRateList);
        //assert
        var resultExchangeRateModels = results.ToList();
        var expectedPriceModels = exchangeRateList.Select(
            e => new PriceModel(e.TargetCurrencyCode, e.Price));
        
        resultExchangeRateModels.Should().HaveCount(1);
        resultExchangeRateModels.Should().BeEquivalentTo(
            new List<ExchangeRateModel>
            {
                new (new CurrencyModel("Bitcoin", "BTC"), expectedPriceModels)
            }
        );

    }
}