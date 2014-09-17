using System;


namespace Duomo.Common.Gunther.Lib
{
    public partial class ScheduledJobSpecification : IScheduledJobSpecification
    {
        #region IScheduledJobSpecification Members

        IScheduleSpecification IScheduledJobSpecification.ScheduleSpecification
        {
            get
            {
                return (IScheduleSpecification)this.Item1;
            }
            set
            {
                this.Item1 = value;
            }
        }

        JobBase IScheduledJobSpecification.JobSpecification
        {
            get
            {
                return this.Item;
            }
            set
            {
                this.Item = value;
            }
        }

        #endregion
    }
}
