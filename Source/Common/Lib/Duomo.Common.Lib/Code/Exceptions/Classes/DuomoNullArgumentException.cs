using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    public class DuomoNullArgumentException : ArgumentException
    {
        public DuomoNullArgumentException()
            : base()
        {
        }

        public DuomoNullArgumentException(string paramName)
            : base("Null parameter.", paramName)
        {
        }

        public DuomoNullArgumentException(string paramName, string message)
            : base(message, paramName)
        {
        }

        public DuomoNullArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected DuomoNullArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
