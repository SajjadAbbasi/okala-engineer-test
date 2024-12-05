using AutoMapper;
using Okala.Infrastructure.ConnectedServices.Exchange.DTOs;
using AppDTOs = Okala.Application.DTOs.ConnectedServices.ExchangeService;

namespace Okala.Infrastructure.Mappings;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateMap<Currency, AppDTOs.ExchangeRate>()
            .ConstructUsing(src => new AppDTOs.ExchangeRate(
                new AppDTOs.Currency(src.Id,src.Name,src.Code),
                src.Quote.First().Key,
                src.Quote.First().Value.Price
                ));
    }
}