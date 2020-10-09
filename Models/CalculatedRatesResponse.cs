using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Models
{
    public class CalculatedRatesResponse
    {
        public string MinRate { get; set; }

        public string MaxRate { get; set; }

        public string AverageRate { get; set; }
    }
}
