using ExchangeRateCalculations.Common.Enums;
using System;

namespace ExchangeRateCalculations.Common.Utils
{
    public static class EnumUtil
    {
        public static string GetValueByName(Type type, byte value)
        {
            return Enum.GetName(type, value); ;
        }

        public static string GetCurrencyByName(Currency value)
        {
            return EnumUtil.GetValueByName(typeof(Currency), (byte)value);
        }
    }
}
