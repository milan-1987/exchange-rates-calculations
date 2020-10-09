using ExchangeRateCalculations.Models;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Service.Interfaces
{
    public interface ICalculatedRatesService
    {
        Task<CalculatedRatesDateRateResponse> GetExhangeRates(CalculatedRatesRequest calculatedRatesRequest);
    }
}
