using System;
using System.Runtime.Serialization;
using System.Security;


namespace Duomo.Common.Lib
{
    public class DuomoWrongTypeException : DuomoException, ISerializable
    {
        #region ISerializable Members

        [SecurityCritical]
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (null == info)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("RequiredType", RequiredType);
            info.AddValue("FoundType", FoundType);

            base.GetObjectData(info, context);
        }

        #endregion


        private static string FormatMessage(Type requiredType, Type foundType)
        {
            return String.Format("Type mismatch. Type required: '{0}, type found: '{1}'.", requiredType.FullName, foundType.FullName);
        }


        private Type zRequiredType;
        public Type RequiredType
        {
            get
            {
                return zRequiredType;
            }
            protected set
            {
                zRequiredType = value;
            }
        }
        private Type zFoundType;
        public Type FoundType
        {
            get
            {
                return zFoundType;
            }
            protected set
            {
                zFoundType = value;
            }
        }


        DuomoWrongTypeException()
            : base()
        {
        }

        DuomoWrongTypeException(string message)
            : base(message)
        {
        }

        DuomoWrongTypeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected DuomoWrongTypeException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            RequiredType = (Type)info.GetValue("RequiredType", typeof(Type));
            FoundType = (Type)info.GetValue("FoundType", typeof(Type));
        }

        public DuomoWrongTypeException(Type requiredType, object foundObject)
            : base(FormatMessage(requiredType, foundObject.GetType()))
        {
            Setup(requiredType, foundObject.GetType());
        }

        public DuomoWrongTypeException(Type requiredType, object foundObject, Exception innerException)
            : base(FormatMessage(requiredType, foundObject.GetType()), innerException)
        {
            Setup(requiredType, foundObject.GetType());
        }

        public DuomoWrongTypeException(Type requiredType, Type foundType)
            : base(FormatMessage(requiredType, foundType))
        {
            Setup(requiredType, foundType);
        }

        public DuomoWrongTypeException(Type requiredType, Type foundType, Exception innerException)
            : base(FormatMessage(requiredType, foundType), innerException)
        {
            Setup(requiredType, foundType);
        }

        private void Setup(Type requiredType, Type foundType)
        {
            RequiredType = requiredType;
            FoundType = foundType;
        }
    }
}
