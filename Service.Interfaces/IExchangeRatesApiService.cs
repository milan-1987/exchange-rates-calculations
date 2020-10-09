using ExchangeRateCalculations.Models;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Service.Interfaces
{
    public interface IExchangeRatesApiService
    {
        Task<CalculatedRatesDateRateResponse> GetRates(CalculatedRatesRequest calculatedRatesRequest);
    }
}
