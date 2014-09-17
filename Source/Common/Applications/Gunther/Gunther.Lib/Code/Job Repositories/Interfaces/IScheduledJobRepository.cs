using System;


namespace Duomo.Common.Gunther.Lib
{
    public interface IJobRepository
    {
        void Add(IJobSpecification jobSpecification, DateTime scheduledTime);
    }
}
