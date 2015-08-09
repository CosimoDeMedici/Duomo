using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class EnumerationValueNotFoundException : Exception, ISerializable
    {
        public const string TypePropertyName = @"Type";
        public const string MissingValuePropertyName = @"MissingValue";


        #region Static

        private static string FormatMessage(Type enumerationType, string missingValue)
        {
            string retValue = String.Format(@"Enumeration '{0}' did not contain '{1}'.", enumerationType.Name, missingValue);
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
            info.AddValue(EnumerationValueUnhandledException.ValuePropertyName, this.MissingValue);

            base.GetObjectData(info, context);
        }

        #endregion


        public Type Type { get; protected set; }
        public string MissingValue { get; protected set; }


        public EnumerationValueNotFoundException()
            : base()
        {
        }

        public EnumerationValueNotFoundException(string message)
            : base(message)
        {
        }

        public EnumerationValueNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public EnumerationValueNotFoundException(Type enumerationType, string missingValue)
            : base(FormatMessage(enumerationType, missingValue))
        {
            this.Setup(enumerationType, missingValue);
        }

        public EnumerationValueNotFoundException(Type enumerationType, string missingValue, Exception innerException)
            : base(FormatMessage(enumerationType, missingValue), innerException)
        {
            this.Setup(enumerationType, missingValue);
        }

        [SecuritySafeCritical]
        protected EnumerationValueNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.Type = (Type)info.GetValue(EnumerationValueUnhandledException.TypePropertyName, typeof(Type));
            this.MissingValue = (string)info.GetValue(EnumerationValueUnhandledException.ValuePropertyName, typeof(string));
        }

        private void Setup(Type enumerationType, string missingValue)
        {
            this.Type = enumerationType;
            this.MissingValue = missingValue;
        }
    }
}
