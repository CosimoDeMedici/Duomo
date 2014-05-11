using System;


namespace Duomo.Common.Lib
{
    public enum RunStatusEnumeration
    {
        Waiting,
        Starting,
        Running,
        Pausing,
        Paused,
        Stopping,
        Stopped,
        Finishing,
        Finished,
        Errored
    }
}
