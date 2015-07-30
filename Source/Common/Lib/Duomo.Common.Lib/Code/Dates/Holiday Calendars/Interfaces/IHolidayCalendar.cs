using System;


namespace Duomo.Common.Lib.Dates
{
    public interface IHolidayCalendar
    {
        bool IsHoliday(DateTime date);
    }
}
