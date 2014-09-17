using System;
using System.Collections.Generic;

using Duomo.Common.Gunther.Lib;


namespace Duomo.Common.Gunther.Forecaster.Lib
{
    public interface IScheduleForecastEmitter
    {
        void EmitSchduleForecast(DateTime startDate, DateTime endDate, List<ScheduledTimeSpecification> scheduledTimes);
    }
}
