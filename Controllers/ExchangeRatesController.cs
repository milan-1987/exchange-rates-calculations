using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExchangeRateCalculations.Common;
using ExchangeRateCalculations.Common.Enums;
using ExchangeRateCalculations.Common.Utils;
using ExchangeRateCalculations.Exceptions;
using ExchangeRateCalculations.Models;
using ExchangeRateCalculations.Service.Interfaces;
using ExchangeRateCalculations.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExchangeRateCalculations.Controllers
{
    [ApiController]
    [Route("api/rates")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly ILogger<ExchangeRatesController> _logger;
        private readonly ICalculatedRatesService _calculatedRatesService;

        public ExchangeRatesController(ILogger<ExchangeRatesController> logger, ICalculatedRatesService calculatedRatesService)
        {
            _logger = logger;
            _calculatedRatesService = calculatedRatesService;
        }

        [HttpGet("get-calculations")]
        [ProducesResponseType(typeof(CalculatedRatesDateRateResponse), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<CalculatedRatesDateRateResponse>> GetCalculatedRates(Currency baseCurrency, Currency targetCurrency, [FromQuery] List<DateTime> dates)
        {
            try
            {
                // doesn't allow duplicate date entries
                if (!ListsUtil.CheckForDuplicates(dates)) throw new DuplicateTypeException("date");

                var request = new CalculatedRatesRequest
                {
                    BasicCurrency = baseCurrency,
                    TargetCurrency = targetCurrency,
                    Dates = dates
                };

                CustomObjectValidator.Validate(request);

                var response = await _calculatedRatesService.GetExhangeRates(request);

                return Ok(response);

            }
            catch(DuplicateTypeException de)
            {
                return BadRequest(de.Message);
            }
            catch(AggregateException ex)
            {
                var messages = ErrorUtil.ValidationErrorMessages(ex);
                return BadRequest(messages);
            }
            catch(EmptyRatesException ere)
            {
                return BadRequest(ere.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
