using System;


namespace Duomo.Common.Gunther.Lib
{
    public class BasicDateOperationProvider : IDateOperationsProvider
    {
        #region IDateOperationsProvider Members

        public bool IsBusinessDay(DateTime date, HolidayCalendarEnumeration holidayCalendar)
        {
            IHolidayCalendar curHolidayCalendar = HolidayCalendarFactory.GetHolidayCalendar(holidayCalendar);

            bool retValue = !curHolidayCalendar.IsHoliday(date);
            return retValue;
        }

        #endregion
    }
}
