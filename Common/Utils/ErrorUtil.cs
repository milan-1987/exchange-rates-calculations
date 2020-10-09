using System;
using System.Collections.Generic;

namespace ExchangeRateCalculations.Common
{
    public static class ErrorUtil
    {
        public static List<string> ValidationErrorMessages(AggregateException ex)
        {
            var messages = new List<string>();

            foreach(var exception in ex.InnerExceptions)
            {
                messages.Add(exception.Message);
            }

            return messages;
        }
    }
}
