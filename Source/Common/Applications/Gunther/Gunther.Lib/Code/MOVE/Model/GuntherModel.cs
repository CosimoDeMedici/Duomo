using System;
using System.Collections.Generic;
using System.Timers;


namespace Duomo.Common.Gunther.Lib
{
    public class GuntherModel
    {
        public List<ScheduledTimeSpecification> ScheduledTimes { get; protected set; }
        public IDateOperationsProvider DateOperationsProvider { get; set; }
        public IJobRepository JobRepository { get; set; }
        private Timer Timer { get; set; }


        public GuntherModel()
        {
            this.ScheduledTimes = new List<ScheduledTimeSpecification>();
        }

        public void AddScheduledJobs(List<IScheduledJobSpecification> scheduledJobSpecifications)
        {
            foreach (IScheduledJobSpecification scheduledJobSpec in scheduledJobSpecifications)
            {
                ScheduledTimeSpecification curTimeSpec = new ScheduledTimeSpecification();
                curTimeSpec.ScheduledJob = scheduledJobSpec;

                this.ScheduledTimes.Add(curTimeSpec);
            }
        }

        public void Start()
        {
            DateTime startTime = DateTime.Now;

            foreach (ScheduledTimeSpecification curScheduledTimeSpec in this.ScheduledTimes)
            {
                curScheduledTimeSpec.ScheduledTime = curScheduledTimeSpec.ScheduledJob.ScheduleSpecification.CalculateNextScheduledTime(startTime, this.DateOperationsProvider);
            }

            this.SetTimer();
        }

        private void SetTimer()
        {
            this.ScheduledTimes.Sort(new ScheduledTimeComparer()); // Oldest to newest (or soonest to latest).

            ScheduledTimeSpecification first = this.ScheduledTimes[0];

            this.Timer = new Timer();
            this.Timer.AutoReset = false;
            TimeSpan span = first.ScheduledTime - DateTime.Now;
            this.Timer.Interval = span.TotalMilliseconds;
            this.Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            this.Timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // Clear so that new timer can be created.
            ((Timer)sender).Elapsed -= Timer_Elapsed;

            DateTime now = DateTime.Now;

            List<ScheduledTimeSpecification> nextScheduledTimes = new List<ScheduledTimeSpecification>();
            for (int iScheduledTime = 0; iScheduledTime < this.ScheduledTimes.Count; iScheduledTime++)
            {
                // Get the first scheduled job.
                ScheduledTimeSpecification curScheduledTimeSpec = this.ScheduledTimes[0];

                // Items are sorted by scheduled time. If we get to the point where we need not go further, don't go further.
                if (TimeSpan.Zero < curScheduledTimeSpec.ScheduledTime - now)
                {
                    break;
                }

                // Remove the first scheduled job.
                this.ScheduledTimes.RemoveAt(0);

                // Create the next item.
                ScheduledTimeSpecification nextScheduledTimeSpec = new ScheduledTimeSpecification();
                nextScheduledTimeSpec.ScheduledJob = curScheduledTimeSpec.ScheduledJob;
                nextScheduledTimeSpec.ScheduledTime = nextScheduledTimeSpec.ScheduledJob.ScheduleSpecification.CalculateNextScheduledTime(now, this.DateOperationsProvider);

                nextScheduledTimes.Add(nextScheduledTimeSpec);

                this.JobRepository.Add(curScheduledTimeSpec.ScheduledJob.JobSpecification, curScheduledTimeSpec.ScheduledTime);
            }

            this.ScheduledTimes.AddRange(nextScheduledTimes);

            this.SetTimer();
        }
    }
}
