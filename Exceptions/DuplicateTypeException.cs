using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Exceptions
{
    public class DuplicateTypeException : Exception
    {
        public DuplicateTypeException()
        {

        }

        public DuplicateTypeException(string type)
            : base($"You have duplicate {type} entries")
        {

        }
    }
}
