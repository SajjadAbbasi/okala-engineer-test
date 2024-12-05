using AutoMapper;
using Okala.WebApi.Models;
using AppDTOs = Okala.Application.DTOs.ConnectedServices.ExchangeService;

namespace Okala.WebApi.Mappings;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {
        CreateMap<IEnumerable<AppDTOs.ExchangeRate>, IEnumerable<ExchangeRateModel>>()
            .ConvertUsing(src => src
                .GroupBy(p => p.BaseCurrency) 
                .Select(group => new ExchangeRateModel(
                    new CurrencyModel(group.Key.Name,group.Key.Code),
                    group.Select(e=>new PriceModel(e.TargetCurrencyCode,e.Price))
                    )
       )); 
    }
}