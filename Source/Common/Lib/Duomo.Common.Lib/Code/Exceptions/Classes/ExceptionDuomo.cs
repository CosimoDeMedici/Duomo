using System;
using System.Runtime.Serialization;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class ExceptionDuomo : Exception
    {
        public ExceptionDuomo()
            : base()
        {
        }

        public ExceptionDuomo(string message)
            : base(message)
        {
        }

        public ExceptionDuomo(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ExceptionDuomo(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
