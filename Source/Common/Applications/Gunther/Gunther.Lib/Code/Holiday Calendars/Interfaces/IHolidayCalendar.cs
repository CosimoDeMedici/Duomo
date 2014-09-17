using System;


namespace Duomo.Common.Gunther.Lib
{
    public interface IHolidayCalendar
    {
        bool IsHoliday(DateTime date);
    }
}
