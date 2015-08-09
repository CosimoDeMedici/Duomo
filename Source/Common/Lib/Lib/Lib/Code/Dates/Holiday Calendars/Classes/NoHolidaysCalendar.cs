using System;


namespace Duomo.Common.Lib.Dates
{
    public class NoHolidaysCalendar : IHolidayCalendar
    {
        #region IHolidayCalendar Members

        public bool IsHoliday(DateTime date)
        {
            return false;
        }

        #endregion
    }
}
