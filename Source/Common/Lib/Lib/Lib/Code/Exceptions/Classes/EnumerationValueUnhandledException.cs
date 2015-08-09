using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class EnumerationValueUnhandledException : Exception, ISerializable
    {
        public const string TypePropertyName = @"Type";
        public const string ValuePropertyName = @"Value";


        #region Static

        private static string FormatMessage(Type enumerationType, Enum value)
        {
            string retValue = String.Format(@"Unhandled value '{0}' of enumeration {1}.", value, enumerationType.Name);
            return retValue;
        }

        #endregion

        #region ISerializable Members

        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException(@"info");
            }

            info.AddValue(EnumerationValueUnhandledException.TypePropertyName, this.Type);
            info.AddValue(EnumerationValueUnhandledException.ValuePropertyName, this.Value);

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
            this.Setup(value.GetType(), value);
        }

        public EnumerationValueUnhandledException(Enum value, Exception innerException)
            : base(FormatMessage(value.GetType(), value), innerException)
        {
            this.Setup(value.GetType(), value);
        }

        [SecuritySafeCritical]
        protected EnumerationValueUnhandledException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Type = (Type)info.GetValue(EnumerationValueUnhandledException.TypePropertyName, typeof(Type));
            this.Value = (Enum)info.GetValue(EnumerationValueUnhandledException.ValuePropertyName, typeof(Enum));
        }

        private void Setup(Type enumerationType, Enum value)
        {
            this.Type = enumerationType;
            this.Value = value;
        }
    }
}
