using System;
using System.Diagnostics;


namespace Duomo.Common.Gunther.Lib
{
    public class BasicJobRepository : IJobRepository
    {
        #region IJobRepository Members

        public void Add(IJobSpecification jobSpecification, DateTime scheduledTime)
        {
            // Just run the job directly.

            SystemProcessCall call = jobSpecification as SystemProcessCall;
            if (null == call)
            {
                throw new ArgumentException(String.Format("Only able to handle job types that are SystemProcessCalls at the moment. Job type found: '{0}'.", jobSpecification.GetType().FullName));
            }

            ProcessStartInfo procStartInfo = new ProcessStartInfo(call.Value);

            Process proc = new Process();
            proc.StartInfo = procStartInfo;
            proc.Start();
        }

        #endregion
    }
}
