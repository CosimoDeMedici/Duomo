using System;


namespace Duomo.Common.Gunther.Lib
{
    public partial class SystemProcessCall : IJobSpecification
    {
        public override string ToString()
        {
            string retValue = String.Format("System process call: '{0}'", this.Value);
            return retValue;
        }
    }
}
