using ExchangeRateCalculations.Common;
using ExchangeRateCalculations.Common.Utils;
using ExchangeRateCalculations.Exceptions;
using ExchangeRateCalculations.Models;
using ExchangeRateCalculations.Service.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ExchangeRateCalculations.Services
{
    [AllowAnonymous]
    public class ExchangeRatesApiService : IExchangeRatesApiService
    {
        private static readonly string baseUri = "https://api.exchangeratesapi.io/history?";

        public async Task<CalculatedRatesDateRateResponse> GetRates(CalculatedRatesRequest calculatedRatesRequest)
        {
            string targetCurrency = EnumUtil.GetCurrencyByName(calculatedRatesRequest.TargetCurrency);
            string baseCurrency = EnumUtil.GetCurrencyByName(calculatedRatesRequest.BasicCurrency);

            string queryString = CreateQueryString(calculatedRatesRequest.Dates.DeepClone(), targetCurrency, baseCurrency);

            var apiResponse = await CallToExchangeRatesApi(queryString);

            var ratesResponse = CreateCalculatedRatesResponse(apiResponse, calculatedRatesRequest.Dates, targetCurrency);

            return ratesResponse;
        }

        private async Task<ExchangeRatesApiResponse> CallToExchangeRatesApi(string queryString)
        {
            var client = new HttpClient();

            var result = await client.GetAsync(queryString);

            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ExchangeRatesApiResponse>(content);
            }

            throw new Exception(message: "Currency not supported");
        }

        #region request
        private string CreateQueryString(List<DateTime> dates, string targetCurrency,
            string baseCurrency)
        {
            string queryString = "";

            List<DateTime> datesForRequest = SetRequestStartEndDates(dates);

            using (var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "start_at", DateTimeUtil.DateTimeToDateString(datesForRequest[0])},
                { "end_at", DateTimeUtil.DateTimeToDateString(datesForRequest[datesForRequest.Count - 1])},
                { "symbols", $"{targetCurrency}" },
                { "base", $"{baseCurrency}" }
            }))
            {
                queryString = baseUri + content.ReadAsStringAsync().Result;
            }

            return queryString;
        }

        // sets request start date to first friday before smallest selected date
        // this is needed because of exchange rates api doesn't return rates for weekends
        private List<DateTime> SetRequestStartEndDates(List<DateTime> dates)
        {
            var datesSorted = DateTimeUtil.SortAscending(dates);

            // to get smallest date rate even if it is sunday
            datesSorted[0] = DateTimeUtil.ChangeDateForSaturdayOrSunday(datesSorted[0]);

            return datesSorted;
        }
        #endregion 

        private CalculatedRatesDateRateResponse CreateCalculatedRatesResponse(ExchangeRatesApiResponse response,
            List<DateTime> dates, string targetCurrency)
        {
            if (response.Rates.Count > 0)
            {
                var converted = new CalculatedRatesDateRateResponse();
                // dictionary of user entered dates and their corresponding days with exchange rate values
                Dictionary<string, string> datesDictionary = new Dictionary<string, string>();
                dates.ForEach(date =>
                {
                    DateTime dateMoved = DateTimeUtil.ChangeDateForSaturdayOrSunday(date);
                    datesDictionary.Add(DateTimeUtil.DateTimeToDateString(date), DateTimeUtil.DateTimeToDateString(dateMoved));
                });

                // filter rates only for user entered days
                // maps response to <string, decimal> dictionary
                var rates = response.Rates.Where(p => datesDictionary.Any(d =>
                {
                    return d.Key == p.Key || d.Value == p.Key;
                }))
                    .ToDictionary(p => p.Key, p => (decimal)p.Value.Fields[targetCurrency])
                    .OrderBy(p => p.Value);

                var minRate = rates.First();
                var maxRate = rates.Last();

                var avg = rates.Select(c => c.Value).Sum() / rates.Count();

                converted.MinRate = new Rate(minRate.Key, minRate.Value); // $"A min rate of {minRate.Value} on {minRate.Key}.";
                converted.MaxRate = new Rate(maxRate.Key, maxRate.Value); // $"A max rate of {maxRate.Value} on {maxRate.Key}.";                
                converted.AverageRate = avg; //  $"An average rate of {avg}.";

                return converted;
            }
            else
            {
                throw new EmptyRatesException();
            }
        }
    }
}
