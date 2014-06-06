using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Duomo.Common.Lib.IO.Serialization
{
    public class BinarySerializer<T> : IFileSerializer<T>
    {
        #region IFileSerializer<T> Members

        public void SerializeToRootedPath(T value, string rootedPath)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            using (Stream fileStream = new FileStream(rootedPath, FileMode.Create))
            {
                binaryFormatter.Serialize(fileStream, value);
            }
        }

        public T DeserializatFromRootedPath(string rootedPath)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            T retValue;
            using (Stream fileStream = new FileStream(rootedPath, FileMode.Open))
            {
                retValue = (T)binaryFormatter.Deserialize(fileStream);
            }

            return retValue;
        }

        #endregion
    }
}
