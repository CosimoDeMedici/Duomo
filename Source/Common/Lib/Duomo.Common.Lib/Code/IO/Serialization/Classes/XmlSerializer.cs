using System;
using System.IO;
using System.Xml.Serialization;


namespace Duomo.Common.Lib.IO.Serialization
{
    public class XmlSerializer<T> : IFileSerializer<T>, IStringSerializer<T>
    {
        #region IFileSerializer<T> Members

        public void SerializeToRootedPath(T value, string rootedPath)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            using (Stream fileStream = new FileStream(rootedPath, FileMode.Open))
            {
                xmlFormatter.Serialize(fileStream, value);
            }
        }

        public T DeserializatFromRootedPath(string rootedPath)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            T retValue;
            using (Stream fileStream = new FileStream(rootedPath, FileMode.Open))
            {
                retValue = (T)xmlFormatter.Deserialize(fileStream);
            }

            return retValue;
        }

        #endregion

        #region IStringSerializer<T> Members

        public string SerializeToString(T value)
        {
            XmlSerializer xmlFormatter = new XmlSerializer(typeof(T));

            StringWriter writer = new StringWriter();
            xmlFormatter.Serialize(writer, value);

            return writer.ToString();
        }

        public T DeserializeFromString(string xml)
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
    }
}
