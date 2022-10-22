using Currency_Api_Handle.Services;
using Currency_Api_Handler.Models.Currency;
using Microsoft.AspNetCore.Mvc;

namespace Currency_Api_Handler.Controllers
{
    [Route("api/")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        public CurrencyService Service { get; }

        public CurrencyController(CurrencyService service)
        {
            Service = service;
        }

        [HttpGet("currencies")]
        public async Task<IActionResult> GetCurrencies(int? elementsSkip = null, int? elementsCount = null)
        {
            try
            {
                IEnumerable<ValutaModel> currencies = await Service.GetDataFromRequestAsync();
                return Ok(currencies.OrderBy(r => r.Name).Skip((elementsSkip ?? 5) * elementsCount ?? 5).Take(elementsCount ?? 5));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("currency/" + "{id}")]
        public async Task<IActionResult> GetCurrencyById(string id)
        {
            try
            {
                var result = (await Service.GetDataFromRequestAsync())
                    .First(v => v.ID == id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}