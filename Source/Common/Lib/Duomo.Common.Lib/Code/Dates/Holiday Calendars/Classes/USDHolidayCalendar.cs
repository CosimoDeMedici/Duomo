using System;
using System.Collections.Generic;


namespace Duomo.Common.Lib.Dates
{
    public class USDHolidayCalendar : IHolidayCalendar
    {
        #region Static

        private static Dictionary<int, List<DateTime>> HolidayDateListsByYear { get; set; }


        static USDHolidayCalendar()
        {
            USDHolidayCalendar.HolidayDateListsByYear = new Dictionary<int, List<DateTime>>();
        }

        private static bool IsHolidayStatic(DateTime date)
        {
            bool retValue = false;
            int year = date.Year;
            if (!USDHolidayCalendar.HolidayDateListsByYear.ContainsKey(year))
            {
                List<DateTime> datesForYear = USDHolidayCalendar.CalculateHolidaysForYear(year);
                USDHolidayCalendar.HolidayDateListsByYear.Add(year, datesForYear);
            }

            if (USDHolidayCalendar.HolidayDateListsByYear[year].Contains(date))
            {
                retValue = true;
            }

            return retValue;
        }

        private static List<DateTime> CalculateHolidaysForYear(int year)
        {
            List<DateTime> retValue = new List<DateTime>();

            retValue.Add(new DateTime(year, 1, 1)); // January 1st.
            retValue.Add(USDHolidayCalendar.CalculateMartinLutherKingDay(year));
            retValue.Add(USDHolidayCalendar.CalculatedPresidentsDay(year));
            retValue.Add(USDHolidayCalendar.CalculateGoodFriday(year)); // Note, Good Friday is not a Federal holiday, but is a SIFMA recommended market close.
            retValue.Add(USDHolidayCalendar.CalculateMemorialDay(year));
            retValue.Add(new DateTime(year, 7, 4)); // July 4th.
            retValue.Add(USDHolidayCalendar.CalculateLaborDay(year));
            retValue.Add(USDHolidayCalendar.CalculateColumbusDay(year));
            retValue.Add(new DateTime(year, 11, 11)); // Veterans Day.
            retValue.Add(USDHolidayCalendar.CalculateThanksgiving(year));
            retValue.Add(new DateTime(year, 12, 25)); // Christmas!

            return retValue;
        }

        private static DateTime CalculateNthDayOfWeekInMonth(int year, int month, int n, DayOfWeek dayOfWeek)
        {
            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            DateTime currentDate = firstDayOfMonth.AddDays(-1);
            int currentN = 0;
            while (n != currentN)
            {
                currentDate = currentDate.AddDays(1);

                if (dayOfWeek == currentDate.DayOfWeek)
                {
                    currentN++;
                }
            }

            return currentDate;
        }

        /// <summary>
        /// The third Monday in January.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>The date of Martin Luther King Day in that year.</returns>
        private static DateTime CalculateMartinLutherKingDay(int year)
        {
            DateTime currentDate = USDHolidayCalendar.CalculateNthDayOfWeekInMonth(year, 1, 3, DayOfWeek.Monday);
            return currentDate;
        }

        /// <summary>
        /// The third Monday in February.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>The date of PresidentsDay (Washington's birthday) in that year.</returns>
        private static DateTime CalculatedPresidentsDay(int year)
        {
            DateTime currentDate = USDHolidayCalendar.CalculateNthDayOfWeekInMonth(year, 2, 3, DayOfWeek.Monday);
            return currentDate;
        }

        /// <summary>
        /// The last Monday in May.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>The date of Memorial Day in that year.</returns>
        private static DateTime CalculateMemorialDay(int year)
        {
            DateTime lastDayOfMay = new DateTime(year, 5, 31);

            DateTime currentDate = lastDayOfMay;
            while (DayOfWeek.Monday != currentDate.DayOfWeek)
            {
                currentDate = currentDate.AddDays(-1);
            }

            return currentDate;
        }

        /// <summary>
        /// The first Monday in September.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>The date of Labor Day in that year.</returns>
        private static DateTime CalculateLaborDay(int year)
        {
            DateTime retValue = USDHolidayCalendar.CalculateNthDayOfWeekInMonth(year, 9, 1, DayOfWeek.Monday);
            return retValue;
        }

        /// <summary>
        /// The second Monday in October.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>The date of Columbus Day in that year.</returns>
        private static DateTime CalculateColumbusDay(int year)
        {
            DateTime retValue = USDHolidayCalendar.CalculateNthDayOfWeekInMonth(year, 10, 2, DayOfWeek.Monday);
            return retValue;
        }

        /// <summary>
        /// The fourth Thursday in November.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>THe date of Thanksgiving in that year.</returns>
        private static DateTime CalculateThanksgiving(int year)
        {
            DateTime retValue = USDHolidayCalendar.CalculateNthDayOfWeekInMonth(year, 11, 4, DayOfWeek.Thursday);
            return retValue;
        }

        /// <summary>
        /// Crazy calculation of Easter Sunday.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>The date of Easter in that year.</returns>
        /// <remarks>Taken from "http://stackoverflow.com/questions/2510383/how-can-i-calculate-what-date-good-friday-falls-on-given-a-year".</remarks>
        private static DateTime CalculateEasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Two days before Easter Sunday.
        /// </summary>
        /// <param name="year">The year of interest.</param>
        /// <returns>The date of Good Friday in that year.</returns>
        private static DateTime CalculateGoodFriday(int year)
        {
            DateTime easterSunday = USDHolidayCalendar.CalculateEasterSunday(year);
            DateTime retValue = easterSunday.AddDays(-2);
            return retValue;
        }

        #endregion

        #region IHolidayCalendar Members

        public bool IsHoliday(DateTime date)
        {
            bool retValue = USDHolidayCalendar.IsHolidayStatic(date);
            return retValue;
        }

        #endregion
    }
}
