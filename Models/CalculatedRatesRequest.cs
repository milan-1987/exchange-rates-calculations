using ExchangeRateCalculations.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExchangeRateCalculations.Models
{
    public class CalculatedRatesRequest
    {
        [Required]
        public List<DateTime> Dates { get; set; }

        [Required]
        [EnumDataType(typeof(Currency))]
        public Currency BasicCurrency { get; set; }

        [Required]
        [EnumDataType(typeof(Currency))]
        public Currency TargetCurrency { get; set; }
    }
}
