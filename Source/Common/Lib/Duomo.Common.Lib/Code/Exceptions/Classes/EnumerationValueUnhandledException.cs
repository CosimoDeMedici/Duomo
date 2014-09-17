using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class EnumerationValueUnhandledException : DuomoException, ISerializable
    {
        #region Static

        private static string FormatMessage(Type enumerationType, Enum value)
        {
            string retValue = String.Format("Unhandled value '{0}' of enumeration {1}.", value, enumerationType.Name);

            return retValue;
        }

        #endregion

        #region ISerializable Members

        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("Type", this.Type);
            info.AddValue("Value", this.Value);

            base.GetObjectData(info, context);
        }

        #endregion


        public Type Type { get; protected set; }
        public Enum Value { get; protected set; }


        public EnumerationValueUnhandledException()
            : base()
        {
        }

        public EnumerationValueUnhandledException(string message)
            : base(message)
        {
        }

        public EnumerationValueUnhandledException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EnumerationValueUnhandledException(Enum value)
            : base(FormatMessage(value.GetType(), value))
        {
            Setup(value.GetType(), value);
        }

        public EnumerationValueUnhandledException(Enum value, Exception innerException)
            : base(FormatMessage(value.GetType(), value), innerException)
        {
            Setup(value.GetType(), value);
        }

        public EnumerationValueUnhandledException(Type enumerationType, Enum value)
            : base(FormatMessage(enumerationType, value))
        {
            Setup(enumerationType, value);
        }

        public EnumerationValueUnhandledException(Type enumerationType, Enum value, Exception innerException)
            : base(FormatMessage(enumerationType, value), innerException)
        {
            Setup(enumerationType, value);
        }

        [SecuritySafeCritical]
        protected EnumerationValueUnhandledException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Type = (Type)info.GetValue("Type", typeof(Type));
            this.Value = (Enum)info.GetValue("Enum", typeof(Enum));
        }

        private void Setup(Type enumerationType, Enum value)
        {
            Type = enumerationType;
            Value = value;
        }
    }
}
