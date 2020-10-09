using System;

namespace ExchangeRateCalculations.Exceptions
{
    [Serializable]
    public class EmptyRatesException : Exception
    {
        public EmptyRatesException()
             : base($"List of rates is empty for requested dates and currencies")
        {

        }
    }
}
