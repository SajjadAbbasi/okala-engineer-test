using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Okala.Application.Interfaces.UseCases;
using Okala.WebApi.Models;

namespace Okala.WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRateController(IExchangeService exchangeService,IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> ListExchangeRate([FromQuery(Name = "code")] string baseCurrencyCode)
    {
        var exchangeRates =await exchangeService.GetExchangeRateByCode(baseCurrencyCode);
        var exchangeRateModels = mapper.Map<IEnumerable<ExchangeRateModel>>(exchangeRates);
        return Ok(exchangeRateModels);
    }
}