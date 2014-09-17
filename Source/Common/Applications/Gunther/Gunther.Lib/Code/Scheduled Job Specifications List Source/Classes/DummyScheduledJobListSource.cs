using System;
using System.Collections.Generic;


namespace Duomo.Common.Gunther.Lib
{
    public class DummyScheduledJobListSource : IScheduledJobSpecificationsListSource
    {
        #region Static

        private static IScheduledJobSpecification CreateDummyScheduledJob()
        {
            IScheduledJobSpecification scheduledJobSpec = new ScheduledJobSpecification();

            SystemProcessCall call = new SystemProcessCall();
            call.Name = "DummyTestJob";
            call.Description = "A hard-coded scheduled job.";
            call.Value = @"C:\Code\DEV\Duomo\Source\Common\Applications\HelloWorld\HelloWorld\bin\Release\Duomo.Common.HelloWorld.exe";

            scheduledJobSpec.JobSpecification = call;

            DailyScheduleSpecification scheduleSpec = new DailyScheduleSpecification();
            scheduleSpec.Time = DateTime.Parse("20:43:00");
            scheduleSpec.DaysOfWeek = "MTWTFSS";
            scheduleSpec.HolidayCalendar = HolidayCalendarEnumeration.NONE;

            scheduledJobSpec.ScheduleSpecification = scheduleSpec;

            return scheduledJobSpec;
        }

        #endregion

        #region IScheduledJobsListSource Members

        public List<IScheduledJobSpecification> ScheduledJobs { get; protected set; }

        #endregion


        public DummyScheduledJobListSource()
        {
            this.ScheduledJobs = new List<IScheduledJobSpecification>();

            IScheduledJobSpecification dummy = DummyScheduledJobListSource.CreateDummyScheduledJob();
            this.ScheduledJobs.Add(dummy);
        }
    }
}
