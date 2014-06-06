using System;


namespace Duomo.Common.Lib.IO.Serialization
{
    public interface IFileSerializer<T>
    {
        void SerializeToRootedPath(T value, string rootedPath);
        T DeserializatFromRootedPath(string rootedPath);
    }
}
