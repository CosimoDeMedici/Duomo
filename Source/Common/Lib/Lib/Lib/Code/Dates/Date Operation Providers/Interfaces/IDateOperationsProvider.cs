using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.Dates
{
    public interface IDateOperationsProvider
    {
        bool IsBusinessDay(DateTime date, IHolidayCalendar holidayCalendar);
        bool IsBusinessDay(DateTime date, List<IHolidayCalendar> holidayCalendars);
    }
}
