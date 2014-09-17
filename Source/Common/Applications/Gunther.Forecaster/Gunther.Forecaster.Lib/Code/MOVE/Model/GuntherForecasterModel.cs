using System;
using System.Collections.Generic;

using Duomo.Common.Gunther.Lib;


namespace Duomo.Common.Gunther.Forecaster.Lib
{
    public class GuntherForecasterModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IDateOperationsProvider DateOperationsProvider { get; set; }
        public IScheduledJobSpecificationsListSource ScheduledJobSpecificationsListSource { get; set; }
        public List<ScheduledTimeSpecification> ScheduledTimeSpecifications { get; protected set; }
        public IScheduleForecastEmitter ForecastEmitter { get; set; }


        public GuntherForecasterModel()
        {
            this.ScheduledTimeSpecifications = new List<ScheduledTimeSpecification>();
        }

        public void CreateScheduledTimeSpecifications()
        {
            // Foreach scheduled job specification from the IScheduledJobSpecificationsListSource.
                // Starting at the StartDate, going to the EndDate, continue rescheduling each job until the scheduled time is past the start date.

            this.ScheduledTimeSpecifications.Clear();

            List<IScheduledJobSpecification> scheduledJobSpecs = this.ScheduledJobSpecificationsListSource.ScheduledJobs;
            foreach (IScheduledJobSpecification curScheduledJobSpec in scheduledJobSpecs)
            {
                DateTime curScheduledTime = this.StartDate;
                while (this.EndDate > curScheduledTime)
                {
                    curScheduledTime = curScheduledJobSpec.ScheduleSpecification.CalculateNextScheduledTime(curScheduledTime, this.DateOperationsProvider);
                    ScheduledTimeSpecification curScheduledTimeSpec = new ScheduledTimeSpecification();
                    this.ScheduledTimeSpecifications.Add(curScheduledTimeSpec);

                    curScheduledTimeSpec.ScheduledTime = curScheduledTime;
                    curScheduledTimeSpec.ScheduledJob = curScheduledJobSpec;
                }
            }
        }

        public void EmitScheduleForecast()
        {
            this.ForecastEmitter.EmitSchduleForecast(this.StartDate, this.EndDate, this.ScheduledTimeSpecifications);
        }
    }
}
