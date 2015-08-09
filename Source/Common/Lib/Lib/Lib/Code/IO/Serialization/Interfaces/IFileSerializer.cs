using System;


namespace Duomo.Common.Lib.IO.Serialization
{
    public interface IFileSerializer<T>
    {
        void SerializeToRootedPathInstance(T value, string rootedPath);
        T DeserializatFromRootedPathInstance(string rootedPath);
    }
}
