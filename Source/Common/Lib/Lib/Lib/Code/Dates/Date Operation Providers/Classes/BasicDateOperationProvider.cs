using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.Dates
{
    public class BasicDateOperationProvider : IDateOperationsProvider
    {
        #region IDateOperationsProvider Members

        public bool IsBusinessDay(DateTime date, IHolidayCalendar holidayCalendar)
        {
            bool retValue = !holidayCalendar.IsHoliday(date);
            return retValue;
        }

        public bool IsBusinessDay(DateTime date, List<IHolidayCalendar> holidayCalendars)
        {
            bool retValue = true;

            foreach (IHolidayCalendar holidayCalendar in holidayCalendars)
            {
                if (holidayCalendar.IsHoliday(date))
                {
                    retValue = false;
                    break;
                }
            }

            return retValue;
        }

        #endregion


        public BasicDateOperationProvider()
        {
        }
    }
}
