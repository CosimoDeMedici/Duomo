using System;


namespace Duomo.Common.Lib.Dates
{
    public static class HolidayCalendarFactory
    {
        public static IHolidayCalendar Create(HolidayCalendarEnumeration holidayCalendar)
        {
            IHolidayCalendar retValue = null;

            switch (holidayCalendar)
            {
                case HolidayCalendarEnumeration.NONE:
                    retValue = new NoHolidaysCalendar();
                    break;

                case HolidayCalendarEnumeration.WKN:
                    retValue = new WeekendHolidayCalendar();
                    break;

                case HolidayCalendarEnumeration.USD:
                    retValue = new USDHolidayCalendar();
                    break;

                default:
                    throw new ArgumentException(String.Format("Unhandled HolidayCalendarEnumeration: '{0}'.", holidayCalendar));
            }

            return retValue;
        }
    }
}
