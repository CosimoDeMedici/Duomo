using System;


namespace Duomo.Common.Gunther.Lib
{
    public static class HolidayCalendarFactory
    {
        public static IHolidayCalendar GetHolidayCalendar(HolidayCalendarEnumeration holidayCalendar)
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

                default:
                    throw new ArgumentException(String.Format("Unhandled HolidayCalendarEnumeration: '{0}'.", holidayCalendar));
            }

            return retValue;
        }
    }
}
