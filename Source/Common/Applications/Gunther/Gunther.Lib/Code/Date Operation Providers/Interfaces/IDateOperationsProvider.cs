using System;


namespace Duomo.Common.Gunther.Lib
{
    public interface IDateOperationsProvider
    {
        bool IsBusinessDay(DateTime date, HolidayCalendarEnumeration holidayCalendar);
    }
}
