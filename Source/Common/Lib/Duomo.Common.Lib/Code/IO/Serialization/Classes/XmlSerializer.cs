using System;
using System.IO;
using System.Xml.Serialization;


namespace Duomo.Common.Lib.IO.Serialization
{
    public class XmlSerializer<T> : IFileSerializer<T>, IStringSerializer<T>
    {
        #region Static

        public static void StaticSerializeToRootedPath(T value, string rootedPath)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            using (Stream fileStream = new FileStream(rootedPath, FileMode.Create))
            {
                xmlFormatter.Serialize(fileStream, value);
            }
        }

        public static T StaticDeserializeFromRootedPath(string rootedPath)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            T retValue;
            using (Stream fileStream = new FileStream(rootedPath, FileMode.Open))
            {
                retValue = (T)xmlFormatter.Deserialize(fileStream);
            }

            return retValue;
        }

        public static string StaticSerializeToString(T value)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            StringWriter writer = new StringWriter();
            xmlFormatter.Serialize(writer, value);

            return writer.ToString();
        }

        public static T StaticDeserializeFromString(string xml)
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

        public void SerializeToRootedPath(T value, string fileRootedPath)
        {
            XmlSerializer<T>.StaticSerializeToRootedPath(value, fileRootedPath);
        }

        public T DeserializatFromRootedPath(string fileRootedPath)
        {
            return XmlSerializer<T>.StaticDeserializatFromRootedPath(fileRootedPath);
        }

        #endregion

        #region IStringSerializer<T> Members

        public string SerializeToString(T value)
        {
            return XmlSerializer<T>.StaticSerializeToString(value);
        }

        public T DeserializeFromString(string xml)
        {
            return XmlSerializer<T>.StaticDeserializeFromString(xml);
        }

        #endregion
    }
}
