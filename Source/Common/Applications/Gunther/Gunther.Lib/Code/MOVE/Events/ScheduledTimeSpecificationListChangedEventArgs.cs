using System;


namespace Duomo.Common.Gunther.Lib
{
    public class ScheduledTimeSpecificationListChangedEventArgs : EventArgs
    {
        public ScheduledTimeSpecification ScheduledTimeSpecification { get; protected set; }


        public ScheduledTimeSpecificationListChangedEventArgs(ScheduledTimeSpecification scheduledJobSpecification)
        {
            this.ScheduledTimeSpecification = scheduledJobSpecification;
        }
    }

    
    public delegate void ScheduledTimeSpecificationListChanged(object sender, ScheduledTimeSpecificationListChangedEventArgs e);
}
