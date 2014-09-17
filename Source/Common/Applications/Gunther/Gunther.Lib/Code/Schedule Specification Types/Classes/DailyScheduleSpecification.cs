using System;


namespace Duomo.Common.Gunther.Lib
{
    public partial class DailyScheduleSpecification : IScheduleSpecification
    {
        #region IScheduleSpecification Members

        public DateTime CalculateNextScheduledTime(DateTime priorScheduledTime, IDateOperationsProvider dateOperationsProvider)
        {
            DateTime retValue = DateTime.MaxValue;

            if (ScheduleOperations.DayIsValidDayForRun(priorScheduledTime, this.DaysOfWeek, this.HolidayCalendar, dateOperationsProvider))
            {
                DateTime scheduledTimeIfToday = this.ComputeScheduledTimeIfOnDay(priorScheduledTime);

                bool alreadyRunToday = scheduledTimeIfToday <= priorScheduledTime;
                if (!alreadyRunToday)
                {
                    retValue = scheduledTimeIfToday;
                    return retValue;
                }
            }

            // Get the next date to run.
            DateTime nextDate = ScheduleOperations.GetNextDate(priorScheduledTime, this.DaysOfWeek, this.HolidayCalendar, dateOperationsProvider);
            retValue = this.ComputeScheduledTimeIfOnDay(nextDate);

            return retValue;
        }

        #endregion


        protected DateTime ComputeScheduledTimeIfOnDay(DateTime day)
        {
            return new DateTime(day.Year, day.Month, day.Day, this.Time.Hour, this.Time.Minute, this.Time.Second);
        }
    }
}
