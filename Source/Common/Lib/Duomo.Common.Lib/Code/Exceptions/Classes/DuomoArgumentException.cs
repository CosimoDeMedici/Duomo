using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    public class DuomoArgumentException : DuomoException, ISerializable
    {
        #region ISerializable Members

        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("ParamName", ParamName);

            base.GetObjectData(info, context);
        }

        #endregion


        private static string FormatMessage(string message, string paramName)
        {
            return String.Format("{0} Parameter name: '{1}'.", message, paramName);
        }


        private string zParamName;
        public string ParamName
        {
            get
            {
                return zParamName;
            }
            protected set
            {
                zParamName = value;
            }
        }


        public DuomoArgumentException()
            : base()
        {
        }

        public DuomoArgumentException(string message)
            : base(message)
        {
        }

        public DuomoArgumentException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        [SecuritySafeCritical]
        protected DuomoArgumentException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            ParamName = (string)info.GetValue("ParamName", typeof(string));
        }

        public DuomoArgumentException(string message, string paramName)
            : base(FormatMessage(message, paramName))
        {
            ParamName = paramName;
        }

        public DuomoArgumentException(string message, string paramName, Exception innerException)
            : base(FormatMessage(message, paramName), innerException)
        {
            ParamName = paramName;
        }
    }
}
