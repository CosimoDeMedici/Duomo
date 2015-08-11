using System;

using Duomo.Common.Lib.Dates;


namespace Duomo.Common.Gunther.Lib
{
    public static class ScheduleOperations
    {
        public static bool DayIsValidDayForRun(DateTime day, string daysOfWeekGrid, HolidayCalendarEnumeration holidayCalendarEnumeration, IDateOperationsProvider dateOperationsProvider)
        {
            // Assume false.
            bool retValue = false;

            if (DaysOfWeekGrid.TrueOnDayOfWeek(day.DayOfWeek, daysOfWeekGrid))
            {
                IHolidayCalendar holidayCalender = HolidayCalendarFactory.Create(holidayCalendarEnumeration);
                if (dateOperationsProvider.IsBusinessDay(day, holidayCalender))
                {
                    retValue = true;
                }
            }

            return retValue;
        }

        public static DateTime GetNextDate(DateTime priorDate, string daysOfWeekGrid, HolidayCalendarEnumeration holidayCalendar, IDateOperationsProvider dateOperationsProvider)
        {
            DateTime retValue = DateTime.MaxValue;

            DateTime initialValue = new DateTime(priorDate.Year, priorDate.Month, priorDate.Day); // Remove the time information.

            // Prevent looping up to year 9999.
            const int maxCounter = 3650; // A decade.
            int counter = 0;

            bool nextDateFound = false;
            while (!nextDateFound)
            {
                DateTime nextDate = initialValue.AddDays(1);
                if (ScheduleOperations.DayIsValidDayForRun(priorDate, daysOfWeekGrid, holidayCalendar, dateOperationsProvider))
                {
                    retValue = nextDate;
                    nextDateFound = true;
                }

                counter++;
                if (maxCounter == counter)
                {
                    throw new ArgumentException(String.Format("Unable to find next date for date {0} after {1} days.", priorDate, maxCounter));
                }
            }

            return retValue;
        }
    }
}
