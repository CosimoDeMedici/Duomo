using System;
using System.Collections.Generic;


namespace Duomo.Common.Gunther.Lib
{
    public interface IScheduledJobSpecificationsListSource
    {
        List<IScheduledJobSpecification> ScheduledJobs { get; }
    }
}
