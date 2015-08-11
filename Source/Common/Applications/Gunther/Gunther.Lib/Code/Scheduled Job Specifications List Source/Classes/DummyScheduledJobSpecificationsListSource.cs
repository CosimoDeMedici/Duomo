using System;
using System.Collections.Generic;

using Duomo.Common.Lib.Dates;


namespace Duomo.Common.Gunther.Lib
{
    public class DummyScheduledJobSpecificationsListSource : IScheduledJobSpecificationsListSource
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
            scheduleSpec.Time = DateTime.Now.AddSeconds(2);// DateTime.Parse("13:03:00");
            scheduleSpec.DaysOfWeek = "MTWTFSS";
            scheduleSpec.HolidayCalendar = HolidayCalendarEnumeration.NONE;

            scheduledJobSpec.ScheduleSpecification = scheduleSpec;

            return scheduledJobSpec;
        }

        private static IScheduledJobSpecification CreateDummyScheduledJob2()
        {
            IScheduledJobSpecification scheduledJobSpec = new ScheduledJobSpecification();

            SystemProcessCall call = new SystemProcessCall();
            call.Name = "DummyTestJob2";
            call.Description = "A hard-coded scheduled job. 2.";
            call.Value = @"C:\Code\DEV\Duomo\Source\Common\Applications\HelloWorld\HelloWorld\bin\Release\Duomo.Common.HelloWorld.exe";

            scheduledJobSpec.JobSpecification = call;

            DailyScheduleSpecification scheduleSpec = new DailyScheduleSpecification();
            scheduleSpec.Time = DateTime.Parse("21:43:00");
            scheduleSpec.DaysOfWeek = "__WTFSS";
            scheduleSpec.HolidayCalendar = HolidayCalendarEnumeration.NONE;

            scheduledJobSpec.ScheduleSpecification = scheduleSpec;

            return scheduledJobSpec;
        }

        #endregion

        #region IScheduledJobsListSource Members

        public List<IScheduledJobSpecification> ScheduledJobs { get; protected set; }

        #endregion


        public DummyScheduledJobSpecificationsListSource()
        {
            this.ScheduledJobs = new List<IScheduledJobSpecification>();

            IScheduledJobSpecification dummy = DummyScheduledJobSpecificationsListSource.CreateDummyScheduledJob();
            this.ScheduledJobs.Add(dummy);

            IScheduledJobSpecification dummy2 = DummyScheduledJobSpecificationsListSource.CreateDummyScheduledJob2();
            this.ScheduledJobs.Add(dummy2);
        }
    }
}
