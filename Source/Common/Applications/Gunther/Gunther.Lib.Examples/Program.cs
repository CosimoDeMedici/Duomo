using System;
using System.Windows.Forms;

using Duomo.Common.Lib.Dates;
using Duomo.Common.Gunther.Lib;


namespace Gunther.Lib.Examples
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Program.DateOperationsExample();
            Program.DateFromScheduleSpecificationExample();
        }

        private static void DateOperationsExample()
        {
            IDateOperationsProvider dateOperationsProvider = new BasicDateOperationProvider();

            // USD holidays. NOTE: does no include weekends.
            IHolidayCalendar usdHolidayCalendar = HolidayCalendarFactory.Create(HolidayCalendarEnumeration.USD);
            
            // Test New Year's Day.
            DateTime newYearsDay2014 = new DateTime(2014, 1, 1);

            bool isHoliday = !dateOperationsProvider.IsBusinessDay(newYearsDay2014, usdHolidayCalendar);
        }

        private static void DateFromScheduleSpecificationExample()
        {
            DailyScheduleSpecification scheduleSpec = new DailyScheduleSpecification();
            scheduleSpec.Time = DateTime.Parse("20:43:00");
            scheduleSpec.DaysOfWeek = "MTWTFSS";
            scheduleSpec.HolidayCalendar = HolidayCalendarEnumeration.NONE;

            IDateOperationsProvider dateOperationsProvider = new BasicDateOperationProvider();

            DateTime runTime = scheduleSpec.CalculateNextScheduledTime(DateTime.Now, dateOperationsProvider);
        }
    }
}
