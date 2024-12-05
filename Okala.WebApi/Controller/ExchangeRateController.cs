using Microsoft.AspNetCore.Mvc;
using Okala.Application.Interfaces.UseCases;

namespace Okala.WebApi.Controller;

[ApiController]
[Route("api/[controller]")]
public class ExchangeRateController(IExchangeService exchangeService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetRate([FromQuery] string code)
    {
        var exchangeRates =await exchangeService.GetExchangeRateByCode(code);
        
        return Ok(exchangeRates);
    }
}