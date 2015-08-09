using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    [Serializable]
    public class WrongTypeException : Exception, ISerializable
    {
        public const string RequiredTypePropertyName = @"RequiredType";
        public const string FoundTypePropertyName = @"FoundType";


        #region Static

        private static string FormatMessage(Type requiredType, Type foundType)
        {
            return String.Format(@"Type mismatch. Type required: '{0}, type found: '{1}'.", requiredType.FullName, foundType.FullName);
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

            info.AddValue(WrongTypeException.RequiredTypePropertyName, this.RequiredType);
            info.AddValue(WrongTypeException.FoundTypePropertyName, this.FoundType);

            base.GetObjectData(info, context);
        }

        #endregion


        public Type RequiredType { get; protected set; }
        public Type FoundType { get; protected set; }


        WrongTypeException()
            : base()
        {
        }

        WrongTypeException(string message)
            : base(message)
        {
        }

        WrongTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public WrongTypeException(Type requiredType, object foundObject)
            : base(FormatMessage(requiredType, foundObject.GetType()))
        {
            this.Setup(requiredType, foundObject.GetType());
        }

        public WrongTypeException(Type requiredType, object foundObject, Exception innerException)
            : base(FormatMessage(requiredType, foundObject.GetType()), innerException)
        {
            this.Setup(requiredType, foundObject.GetType());
        }

        public WrongTypeException(Type requiredType, Type foundType)
            : base(FormatMessage(requiredType, foundType))
        {
            this.Setup(requiredType, foundType);
        }

        public WrongTypeException(Type requiredType, Type foundType, Exception innerException)
            : base(FormatMessage(requiredType, foundType), innerException)
        {
            this.Setup(requiredType, foundType);
        }

        [SecuritySafeCritical]
        protected WrongTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.RequiredType = (Type)info.GetValue(WrongTypeException.RequiredTypePropertyName, typeof(Type));
            this.FoundType = (Type)info.GetValue(WrongTypeException.FoundTypePropertyName, typeof(Type));
        }

        private void Setup(Type requiredType, Type foundType)
        {
            this.RequiredType = requiredType;
            this.FoundType = foundType;
        }
    }
}
