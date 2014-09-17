using System;


namespace Duomo.Common.Gunther.Lib
{
    public interface IScheduleSpecification
    {
        DateTime CalculateNextScheduledTime(DateTime priorScheduledTime, IDateOperationsProvider dateOperationsProvider);
    }
}
