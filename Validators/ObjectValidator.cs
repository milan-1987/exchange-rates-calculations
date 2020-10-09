using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Validators
{
    public static class CustomObjectValidator
    {
        public static bool Validate(object o)
        {
            var ctx = new ValidationContext(o, null, null);
            var errors = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(o, ctx, errors, true);

            if (!isValid)
            {
                throw new AggregateException(
                    errors.Select((e) => new ValidationException(e.ErrorMessage))
                    );
            }

            return true;
        }
    }    
}
