using System;


namespace Duomo.Common.Lib
{
    public static class DateTimeExtensions
    {
        public static string ToYYYYMMDD_String(this DateTime dateTime)
        {
            string retValue = String.Format(@"{0:yyyyMMdd}", dateTime);
            return retValue;
        }

        public static int ToYYYYMMDD_Int(this DateTime dateTime)
        {
            int retValue = dateTime.Year * 10000 + dateTime.Month * 100 + dateTime.Day;
            return retValue;
        }

        public static double ToYYYYMMDD_Double(this DateTime dateTime)
        {
            double retValue = Convert.ToDouble(dateTime.ToYYYYMMDD_Int()) + (dateTime.Hour * 3600 + dateTime.Minute * 60 + dateTime.Second + dateTime.Millisecond / 1000) / (24 * 3600);
            return retValue;
        }
    }
}
