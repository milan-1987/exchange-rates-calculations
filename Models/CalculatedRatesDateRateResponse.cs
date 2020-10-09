using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Models
{
    public class CalculatedRatesDateRateResponse
    {
        public Rate MinRate { get; set; }

        public Rate MaxRate { get; set; }

        public decimal AverageRate { get; set; }
    }

    public class Rate
    {
        public Rate(string date, decimal value)
        {
            Date = date;
            Value = value;
        }

        public string Date { get; set; }

        public decimal Value { get; set; }
    }
}
