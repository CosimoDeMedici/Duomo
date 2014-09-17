using System;


namespace Duomo.Common.Gunther.Lib
{
    public interface IScheduledJobSpecification
    {
        IScheduleSpecification ScheduleSpecification { get; set; }
        JobBase JobSpecification { get; set; }
    }
}
