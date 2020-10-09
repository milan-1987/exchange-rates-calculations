using ExchangeRateCalculations.Models;
using ExchangeRateCalculations.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Handlers
{
    [AllowAnonymous]
    public class CalculatedRatesService : ICalculatedRatesService
    {
        private readonly IExchangeRatesApiService _exchangeRatesService;

        public CalculatedRatesService(IExchangeRatesApiService exchangeRatesService)
        {
            _exchangeRatesService = exchangeRatesService;
        }

        public async Task<CalculatedRatesDateRateResponse> GetExhangeRates(CalculatedRatesRequest calculatedRatesRequest)
        {
            var response = await _exchangeRatesService.GetRates(calculatedRatesRequest);

            return response;
        }               
    }
}
