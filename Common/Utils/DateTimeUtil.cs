using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExchangeRateCalculations.Common
{
    public static class DateTimeUtil
    {
        private static string format = "yyyy-MM-dd";

        public static string DateTimeToDateString(DateTime dateTime)
        {
            return dateTime.ToString(format);
        }

        public static List<DateTime> SortAscending(List<DateTime> list)
        {
            list.Sort((a, b) => a.CompareTo(b));
            return list;
        }

        public static DateTime ChangeDateForSaturdayOrSunday(DateTime date)
        {
            var dayOfWeek = date.DayOfWeek;

            if (dayOfWeek.Equals(DayOfWeek.Sunday)) return date.AddDays(-2);
            else if (dayOfWeek.Equals(DayOfWeek.Saturday)) return date.AddDays(-1);            

            return date;
        }
    }
}
