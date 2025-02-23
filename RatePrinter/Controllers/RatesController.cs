using Domain.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using RatePrinter.Exceptions;
using RatePrinter.Models;
using RatePrinter.Services;

namespace RatePrinter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatesController : Controller
    {
        private readonly IExchangeRateService _exchangeRateService;
        public RatesController(IExchangeRateService exchangeRateService)
        {
            _exchangeRateService = exchangeRateService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatePair>>> GetAllRates()
        {
            try
            {
                var rates = await _exchangeRateService.GetAllExchangeRatesAsync();
                return Ok(rates);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred" });
            }
        }

        [HttpGet("Pair")]
        public async Task<ActionResult<RatePair>> GetRate([FromQuery]GetRateRequest request)
        {
            try
            {
                var rate = await _exchangeRateService.GetExchangeRateAsync(request);
                return Ok(rate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "An unexpected error occurred" });
            }
        }
    }
}
