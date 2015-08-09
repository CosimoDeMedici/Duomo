using System;
using System.IO;
using System.Xml.Serialization;


namespace Duomo.Common.Lib.IO.Serialization
{
    public class XmlSerializer<T> : IFileSerializer<T>, IStringSerializer<T>
    {
        #region Static

        public static void SerializeToRootedPath(T value, string rootedPath)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            using (Stream fileStream = new FileStream(rootedPath, FileMode.Create))
            {
                xmlFormatter.Serialize(fileStream, value);
            }
        }

        public static T DeserializeFromRootedPath(string rootedPath)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            T retValue;
            using (Stream fileStream = new FileStream(rootedPath, FileMode.Open))
            {
                retValue = (T)xmlFormatter.Deserialize(fileStream);
            }

            return retValue;
        }

        public static string SerializeToString(T value)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            StringWriter writer = new StringWriter();
            xmlFormatter.Serialize(writer, value);

            return writer.ToString();
        }

        public static T DeserializeFromString(string xml)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            T retValue;
            using (Stream stream = xml.ToStream())
            {
                retValue = (T)xmlFormatter.Deserialize(stream);
            }
            
            return retValue;
        }

        #endregion

        #region IFileSerializer<T> Members

        public void SerializeToRootedPathInstance(T value, string fileRootedPath)
        {
            XmlSerializer<T>.SerializeToRootedPath(value, fileRootedPath);
        }

        public T DeserializatFromRootedPathInstance(string fileRootedPath)
        {
            T retValue = XmlSerializer<T>.DeserializeFromRootedPath(fileRootedPath);
            return retValue;
        }

        #endregion

        #region IStringSerializer<T> Members

        public string SerializeToStringInstance(T value)
        {
            string retValue = XmlSerializer<T>.SerializeToString(value);
            return retValue;
        }

        public T DeserializeFromStringInstance(string xml)
        {
            T retValue = XmlSerializer<T>.DeserializeFromString(xml);
            return retValue;
        }

        #endregion
    }
}
