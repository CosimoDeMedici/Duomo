using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class DuomoException : Exception
    {
        public DuomoException()
            : base()
        {
        }

        public DuomoException(string message)
            : base(message)
        {
        }

        public DuomoException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected DuomoException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
