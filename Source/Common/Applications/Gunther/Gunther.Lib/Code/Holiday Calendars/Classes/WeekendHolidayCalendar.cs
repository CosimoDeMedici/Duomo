using System;


namespace Duomo.Common.Gunther.Lib
{
    public class WeekendHolidayCalendar : IHolidayCalendar
    {
        #region IHolidayCalendar Members

        public bool IsHoliday(DateTime date)
        {
            bool retValue = date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
            return retValue;
        }

        #endregion
    }
}
