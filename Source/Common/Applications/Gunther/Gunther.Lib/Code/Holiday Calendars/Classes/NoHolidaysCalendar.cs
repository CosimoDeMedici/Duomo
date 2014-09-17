using System;


namespace Duomo.Common.Gunther.Lib
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
