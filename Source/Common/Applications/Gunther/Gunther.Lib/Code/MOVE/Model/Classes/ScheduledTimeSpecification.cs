using System;
using System.Collections.Generic;


namespace Duomo.Common.Gunther.Lib
{
    public class ScheduledTimeSpecification
    {
        public DateTime ScheduledTime { get; set; }
        public IScheduledJobSpecification ScheduledJob { get; set; }
    }


    class ScheduledTimeComparer : IComparer<ScheduledTimeSpecification>
    {
        #region IComparer<ScheduledTimeSpecification> Members

        public int Compare(ScheduledTimeSpecification x, ScheduledTimeSpecification y)
        {
            return x.ScheduledTime.CompareTo(y.ScheduledTime);
        }

        #endregion
    }
}
