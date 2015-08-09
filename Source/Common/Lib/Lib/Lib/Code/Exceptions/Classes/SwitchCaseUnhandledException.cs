using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    public class SwitchCaseUnhandledException : Exception, ISerializable
    {
        public const string UnhandledValuePropertyName = @"UnhandledValue";


        #region Static

        private static string FormatMessage(object value)
        {
            string valueString = value.ToString();

            string retValue = String.Format(@"Unhandled switch case value: '{0}'.", valueString);
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

            info.AddValue(SwitchCaseUnhandledException.UnhandledValuePropertyName, this.UnhandledValue);

            base.GetObjectData(info, context);
        }

        #endregion


        public object UnhandledValue { get; protected set; }


        public SwitchCaseUnhandledException() : base() { }

        public SwitchCaseUnhandledException(string message) : base(message) { }

        public SwitchCaseUnhandledException(string message, Exception innerException) : base(message, innerException) { }

        public SwitchCaseUnhandledException(object unhandledValue)
            : base(SwitchCaseUnhandledException.FormatMessage(unhandledValue))
        {
            this.Setup(unhandledValue);
        }

        public SwitchCaseUnhandledException(object unhandledValue, Exception innerException)
            : base(SwitchCaseUnhandledException.FormatMessage(unhandledValue), innerException)
        {
            this.Setup(unhandledValue);
        }

        [SecuritySafeCritical]
        protected SwitchCaseUnhandledException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.UnhandledValue = info.GetValue(SwitchCaseUnhandledException.UnhandledValuePropertyName, typeof(object));
        }

        private void Setup(object unhandledValue)
        {
            this.UnhandledValue = unhandledValue;
        }
    }
}
