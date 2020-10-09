using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Models
{
    public class ExchangeRatesApiResponse
    {
        [JsonProperty(PropertyName = "rates", Order = 1)]
        public Dictionary<string, ExchangeRatesApiResponseRate> Rates { get; set; }

        [JsonProperty(PropertyName = "start_at", Order = 2)]
        public DateTime StartAt { get; set; }

        [JsonProperty(PropertyName = "base", Order = 3)]
        public string Base { get; set; }

        [JsonProperty(PropertyName = "end_at", Order = 4)]
        public DateTime EndAt { get; set; }
    }

    public class ExchangeRatesApiResponseRate
    {
        [JsonExtensionData]
        public SortedDictionary<string, JToken> Fields { get; set; }
    }
}
