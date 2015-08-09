using System;


namespace Duomo.Common.Lib
{
    public static class IntExtensions
    {
        public static DateTime ToDateTimeFromYYYYMMDD(this int yyyymmdd)
        {
            DateTime retValue = yyyymmdd.ToString().ToDateTimeFromYYYYMMDD();
            return retValue;
        }
    }
}
