using System;


namespace Duomo.Common.Lib.IO.Serialization
{
    public interface IStringSerializer<T>
    {
        string SerializeToString(T value);
        T DeserializeFromString(string str);
    }
}
